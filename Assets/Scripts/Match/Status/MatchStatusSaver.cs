using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DB;
using Assets.Scripts.DB.Documents;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Map;
using Assets.Scripts.Match.Status.Entities;
using Assets.Scripts.Match.Status.Entities.Match;
using UnityEngine;

namespace Assets.Scripts.Match.Status
{
    public class MatchStatusSaver
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;
        private TurnManager _turnManager;

        public MatchStatusSaver(
            BoardManager boardManager,
            MatchManager matchManager,
            TurnManager turnManager)
        {
            ArgumentValidator.ValidateArgumentNotNull(boardManager, "boardManager");
            ArgumentValidator.ValidateArgumentNotNull(matchManager, "matchManager");
            ArgumentValidator.ValidateArgumentNotNull(matchManager, "matchManager");

            _boardManager = boardManager;
            _matchManager = matchManager;
            _turnManager = turnManager;
        }

        public void SaveStatus(string matchId)
        {
            var status = BuildStatus();
            
            DbManager.instance.Context.SaveAsync(new MatchDocument
            {
                MatchId = matchId,
                StateJson = JsonUtility.ToJson(status)
            }, (result) => {
                if (result.Exception == null)
                    Debug.Log("Status saved.");
                else
                    Debug.Log("Could not save status. Reason: " + result.Exception);
            });
        }

        private MatchStatus BuildStatus()
        {
            return new MatchStatus
            {
                Tiles = GetTiles().ToList(),
                PlayerTokens = GetPlayerTokens().ToList(),
                Players = GetPlayers().ToList(),
                CurrentPlayer = _matchManager.CurrentPlayerIndex,
                TurnPlayed = _turnManager.HasTurnBeenPlayed,
                Farms = GetFarms().ToList()
            };
        }

        private IEnumerable<Entities.Match.Player> GetPlayers()
        {
            return _matchManager.Players.Select(player => new Player
            {
                Id = player.Id,
                Gold = player.Gold
            });
        }

        private IEnumerable<Entities.Match.Tile> GetTiles()
        {
            return _boardManager.Tiles.Select(tile => new Entities.Match.Tile
            {
                Id = tile.Id,
                Position = tile.transform.position,
                Uncovered = tile.IsUncovered,
                TileType = tile.Type,
                Digged = tile.IsDigged
            });
        }

        private IEnumerable<PlayerToken> GetPlayerTokens()
        {
            return _boardManager.PlayerTokens.Select(playerToken => new PlayerToken
            {
                PlayerId = playerToken.PlayerId,
                TileId = playerToken.Tile.Id,
                Color = playerToken.Color
            });
        }

        private IEnumerable<Farm> GetFarms()
        {
            return Object.FindObjectsOfType<Map.Locations.Farm>().Select(
                f => new Farm
                {
                    TileId = f.Tile.Id,
                    PlayerId = f.PlayerId
                });
        }
    }
}
