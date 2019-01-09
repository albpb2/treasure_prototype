using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Match.Status;
using Assets.Scripts.Match.Status.Entities.Match;
using Assets.Scripts.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PlayerToken = Assets.Scripts.Players.PlayerToken;

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

        private BoardManager _boardManager;
        private MatchStatusSaver _matchStatusSaver;
        private TileFactory _tileFactory;
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
                CurrentPlayerId = PlayerIds[_currentPlayerIndex];

                CurrentPlayerToken = _boardManager.FindPlayerToken(CurrentPlayerId);

                _playerInfoPanel.SetPlayerInfo(CurrentPlayerToken);

                if (onCurrentPlayerChanged != null)
                {
                    onCurrentPlayerChanged();
                }
            }
        }

        public long CurrentPlayerId { get; set; }

        public Players.PlayerToken CurrentPlayerToken { get; set; }

        public bool Pause { get; set; }

        public List<long> PlayerIds { get; set; }

        public string MatchId { get; set; }

        public delegate void PlayerChange();

        public static event PlayerChange onCurrentPlayerChanged;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchStatusSaver = new MatchStatusSaver(_boardManager, this);
            _tileFactory = new TileFactory();

            PlayerIds = new List<long>();

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

        public void SetStatus(MatchStatus matchStatus)
        {
            PlayerIds = matchStatus.Players.Select(player => player.Id).ToList();
            CurrentPlayer = PlayerIds.IndexOf(matchStatus.CurrentPlayer);
            
            _boardManager.Tiles = RecreateTilesFromMatchStatus(matchStatus);
            
            _boardManager.PlayerTokens = RecreatePlayerTokensFromMatchStatus(matchStatus);
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
            CurrentPlayer = (CurrentPlayer + 1) % PlayerIds.Count;
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
            PlayerIds.Add(playerId);
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

        private List<Map.Tile> RecreateTilesFromMatchStatus(MatchStatus matchStatus)
        {
            var oldTiles = FindObjectsOfType<Map.Tile>();
            foreach (var tile in oldTiles)
            {
                Destroy(tile);
            }

            var tiles = new List<Map.Tile>();
            foreach (var statusTile in matchStatus.Tiles)
            {
                var tile = _tileFactory.CreateTile(statusTile.TileType, statusTile.Position.x, statusTile.Position.y, statusTile.Id);
                if (statusTile.Uncovered)
                {
                    tile.Uncover();
                }
                tiles.Add(tile);
            }

            return tiles;
        }

        private List<PlayerToken> RecreatePlayerTokensFromMatchStatus(MatchStatus matchStatus)
        {
            var oldPlayerTokens = FindObjectsOfType<PlayerToken>();
            foreach (var playerToken in oldPlayerTokens)
            {
                Destroy(playerToken);
            }

            var playerTokens = new List<PlayerToken>();
            foreach (var statusPlayerToken in matchStatus.PlayerTokens)
            {
                var playerToken = PlayerToken.CreatePlayerToken(statusPlayerToken.PlayerId, statusPlayerToken.Color,
                    _boardManager.FindTile(statusPlayerToken.TileId));
                playerTokens.Add(playerToken);
            }

            return playerTokens;
        }
    }
}
