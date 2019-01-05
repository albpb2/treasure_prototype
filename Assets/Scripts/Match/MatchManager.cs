using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Match.Status;
using Assets.Scripts.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Match
{
    public class MatchManager : MonoBehaviour
    {
        private const int MinNumberOfPlayers = 1;
        private const int MaxNumberOfPlayers = 4;

        [SerializeField]
        private PlayerInfoPanel _playerInfoPanel;
        [SerializeField]
        private GameObject _numberOfPlayersPanel;
        [SerializeField]
        private Text _numberOfPlayersText;

        private List<long> _playersIds;
        private BoardManager _boardManager;
        private MatchStatusSaver _matchStatusSaver;
        private int _currentPlayerIndex;

        public int CurrentPlayer
        {
            get
            {
                return _currentPlayerIndex;
            }
            set
            {
                _currentPlayerIndex = value;
                CurrentPlayerId = _playersIds[_currentPlayerIndex];

                CurrentPlayerToken = _boardManager.FindPlayerToken(CurrentPlayerId);

                _playerInfoPanel.SetPlayerInfo(CurrentPlayerToken);

                if (onCurrentPlayerChanged != null)
                {
                    onCurrentPlayerChanged();
                }
            }
        }

        public long CurrentPlayerId { get; set; }

        public PlayerToken CurrentPlayerToken { get; set; }

        public bool Pause { get; set; }

        public List<long> PlayerIds {
            get
            {
                return _playersIds;
            }
        }

        public string MatchId { get; set; }

        public delegate void PlayerChange();

        public static event PlayerChange onCurrentPlayerChanged;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchStatusSaver = new MatchStatusSaver(_boardManager, this);

            _playersIds = new List<long>();

            Pause = true;

            MatchId = Guid.NewGuid().ToString();
        }

        public void StartMatch()
        {
            var numberOfPlayers = GetNumberOfPlayers();

            if (IsNumberOfPlayersCorrect(numberOfPlayers))
            {
                CreatePlayers(numberOfPlayers);

                DisableNumberOfPlayersPanel();

                EnablePlayerInfoPanel();

                CurrentPlayer = Enumerable.Range(0, numberOfPlayers).ToList().GetRandomElement();

                Pause = false;
            }
        }

        public void CreatePlayers(int numberOfPlayers)
        {
            for (var i = 0; i < numberOfPlayers; i++)
            {
                CreatePlayer(i);
            }
        }

        private int GetNumberOfPlayers()
        {
            int numberOfPlayers = 0;

            if (_numberOfPlayersText != null)
            {
                int.TryParse(_numberOfPlayersText.text, out numberOfPlayers);
            }

            return numberOfPlayers;
        }

        public void SwitchCurrentPlayer()
        {
            CurrentPlayer = (CurrentPlayer + 1) % _playersIds.Count;
        }

        public void SaveStatus()
        {
            _matchStatusSaver.SaveStatus(MatchId);
        }

        private bool IsNumberOfPlayersCorrect(int numberOfPlayers)
        {
            return numberOfPlayers >= MinNumberOfPlayers && numberOfPlayers <= MaxNumberOfPlayers;
        }

        private void CreatePlayer(int playerId)
        {
            _playersIds.Add(playerId);
            _boardManager.CreatePlayerToken(playerId);
        }

        private void EnablePlayerInfoPanel()
        {
            _playerInfoPanel.gameObject.SetActive(true);
        }

        private void DisableNumberOfPlayersPanel()
        {
            _numberOfPlayersPanel.SetActive(false);
        }
    }
}
