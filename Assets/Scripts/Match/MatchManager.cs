using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Match.Status;
using Assets.Scripts.Match.Status.Entities.Match;
using Assets.Scripts.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map.Locations;
using UnityEngine;
using UnityEngine.UI;
using Player = Assets.Scripts.Players.Player;
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
        [SerializeField]
        private int _goldPerMovement;
        [SerializeField]
        private int _goldPerDig = 20;
        [SerializeField]
        private int _farmCost = 40;
        [SerializeField]
        private int _farmMoneyPerTurn = 10;

        private BoardManager _boardManager;
        private MatchStatusSaver _matchStatusSaver;
        private TileFactory _tileFactory;
        private TurnManager _turnManager;
        private int _currentPlayerIndex;

        public int CurrentPlayerIndex
        {
            get
            {
                return _currentPlayerIndex;
            }
            set
            {
                _currentPlayerIndex = value;
                CurrentPlayerId = Players[_currentPlayerIndex].Id;

                CurrentPlayerToken = _boardManager.FindPlayerToken(CurrentPlayerId);

                _playerInfoPanel.SetPlayerInfo(CurrentPlayer, CurrentPlayerToken);

                onCurrentPlayerChanged?.Invoke();
            }
        }

        public Player CurrentPlayer => Players[CurrentPlayerIndex];

        public long CurrentPlayerId { get; set; }

        public Players.PlayerToken CurrentPlayerToken { get; set; }

        public bool Pause { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public string MatchId { get; set; }

        public int GoldPerMovement => _goldPerMovement;

        public int GoldPerDig => _goldPerDig;

        public int FarmCost => _farmCost;

        public int FarmMoneyPerTurn => _farmMoneyPerTurn;

        public PlayerInfoPanel PlayerInfoPanel => _playerInfoPanel;

        public delegate void PlayerChange();

        public static event PlayerChange onCurrentPlayerChanged;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _turnManager = FindObjectOfType<TurnManager>();
            _matchStatusSaver = new MatchStatusSaver(_boardManager, this, _turnManager);
            _tileFactory = new TileFactory();

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

                CurrentPlayerIndex = Enumerable.Range(0, numberOfPlayers).ToList().GetRandomElement();

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
            Players = matchStatus.Players.Select(statusPlayer => new Player(statusPlayer.Id)
            {
                Gold = statusPlayer.Gold
            }).ToList();
            CurrentPlayerIndex = Players.IndexOf(Players.Single(player => player.Id == matchStatus.CurrentPlayer));
            
            _boardManager.Tiles = RecreateTilesFromMatchStatus(matchStatus);
            
            _boardManager.PlayerTokens = RecreatePlayerTokensFromMatchStatus(matchStatus);

            RecreateFarmsFromMatchStatus(matchStatus);

            if (matchStatus.TurnPlayed)
            {
                _turnManager.PlayTurn();
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
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        }

        public void SaveStatus()
        {
            _matchStatusSaver.SaveStatus(MatchId);
        }

        public Player FindPlayer(long playerId)
        {
            return Players.SingleOrDefault(p => p.Id == playerId);
        }

        private bool IsNumberOfPlayersCorrect(int numberOfPlayers)
        {
            return numberOfPlayers >= MinNumberOfPlayers && numberOfPlayers <= MaxNumberOfPlayers;
        }

        private void CreatePlayer(int playerId)
        {
            var player = new Player(playerId);
            Players.Add(player);
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
                if (statusTile.Digged)
                {
                    tile.Dig();
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

        public void RecreateFarmsFromMatchStatus(MatchStatus matchStatus)
        {
            var oldFarms = FindObjectsOfType<Farm>();
            foreach (var oldFarm in oldFarms)
            {
                Destroy(oldFarm);
            }

            foreach (var matchStatusFarm in matchStatus.Farms)
            {
                var tile = _boardManager.FindTile(matchStatusFarm.TileId);
                var playerToken = _boardManager.FindPlayerToken(matchStatusFarm.PlayerId);
                Farm.Instantiate(tile, playerToken);
            }
        }
    }
}
