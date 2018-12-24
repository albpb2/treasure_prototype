using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Players;
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
        
        private List<long> _playersIds;
        private BoardManager _boardManager;
        private int _currentPlayerIndex;
        private PlayerInfoPanel _playerInfoPanel;

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
                _playerInfoPanel.SetPlayerInfo(_boardManager.FindPlayerToken(CurrentPlayerId));
            }
        }

        public long CurrentPlayerId { get; set; }

        public bool Pause { get; set; }

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _playerInfoPanel = FindObjectOfType<PlayerInfoPanel>();

            _playersIds = new List<long>();

            Pause = true;
        }

        public void CreatePlayers()
        {
            var numberOfPlayers = GetNumberOfPlayers();

            if (IsNumberOfPlayersCorrect(numberOfPlayers))
            {
                for (var i = 0; i < numberOfPlayers; i++)
                {
                    CreatePlayer(i);
                }

                DisableNumberOfPlayersPanel();

                Pause = false;
            }

            CurrentPlayer = Enumerable.Range(0, numberOfPlayers).ToList().GetRandomElement();
        }

        private int GetNumberOfPlayers()
        {
            var numberOfPlayersText = GameObject.Find("NumberOfPlayersText").GetComponent<Text>();
            int numberOfPlayers = 0;

            if (numberOfPlayersText != null)
            {
                int.TryParse(numberOfPlayersText.text, out numberOfPlayers);
            }

            return numberOfPlayers;
        }

        public void SwitchCurrentPlayer()
        {
            CurrentPlayer = (CurrentPlayer + 1) % _playersIds.Count;
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

        private void DisableNumberOfPlayersPanel()
        {
            var numberOfPlayersPanel = GameObject.Find("NumberOfPlayersPanel");

            numberOfPlayersPanel.SetActive(false);
        }
    }
}
