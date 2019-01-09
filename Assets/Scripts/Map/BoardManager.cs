using Assets.Scripts.Extensions;
using Assets.Scripts.Players;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class BoardManager : MonoBehaviour
    {
        public List<Tile> Tiles { get; set; }
        public List<PlayerToken> PlayerTokens { get; set; }

        private void Awake()
        {
            var cellsSet = GameObject.FindGameObjectWithTag(Tags.Tiles);

            if (cellsSet == null)
            {
                var boardGenerator = new BoardGenerator();
                boardGenerator.PaintBoard();

                cellsSet = GameObject.FindGameObjectWithTag(Tags.Tiles);
            }

            Tiles = cellsSet.GetComponentsInChildren<Tile>().ToList();

            if (Tiles.Count == 0)
            {
                var boardGenerator = new BoardGenerator();
                boardGenerator.PaintBoard();

                Tiles = cellsSet.GetComponentsInChildren<Tile>().ToList();
            }

            PlayerTokens = new List<PlayerToken>();
        }

        public PlayerToken FindPlayerToken(long playerId)
        {
            return PlayerTokens.FirstOrDefault(playerToken => playerToken.PlayerId == playerId);
        }

        public Tile FindTile(int tileId)
        {
            return Tiles.FirstOrDefault(tile => tile.Id == tileId);
        }

        public void CreatePlayerToken(long playerId)
        {
            var playerToken = PlayerToken.CreatePlayerToken(playerId, GetRandomEmptyTile());
            PlayerTokens.Add(playerToken);
        }

        private Tile GetRandomEmptyTile()
        {
            var playerInitialPosition = Tiles.GetRandomElement();

            while (playerInitialPosition.PlayerToken != null)
            {
                playerInitialPosition = Tiles.GetRandomElement();
            }

            return playerInitialPosition;
        }
    }
}
