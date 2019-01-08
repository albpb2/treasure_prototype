using Assets.Scripts.DB;
using Assets.Scripts.DB.Documents;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Map;
using Assets.Scripts.Match.Status.Entities.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Match.Status
{
    public class MatchStatusSaver
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;
        
        public MatchStatusSaver(BoardManager boardManager, MatchManager matchManager)
        {
            ArgumentValidator.ValidateArgumentNotNull(boardManager, "boardManager");
            ArgumentValidator.ValidateArgumentNotNull(matchManager, "matchManager");

            _boardManager = boardManager;
            _matchManager = matchManager;
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
                Players = _matchManager.PlayerIds.Select(playerId => new Player { Id = playerId }).ToList(),
                CurrentPlayer = _matchManager.CurrentPlayer
            };
        }

        private IEnumerable<Entities.Match.Tile> GetTiles()
        {
            return _boardManager.Tiles.Select(tile => new Entities.Match.Tile
            {
                Id = tile.Id,
                Position = tile.transform.position,
                Uncovered = tile.IsUncovered,
                TileType = tile.Type
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
    }
}
