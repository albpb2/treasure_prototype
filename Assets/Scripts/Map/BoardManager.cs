using Assets.Scripts.Extensions;
using Assets.Scripts.Players;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class BoardManager : MonoBehaviour
    {
        public List<Tile> Tiles { get; private set; }
        public List<PlayerToken> PlayerTokens { get; private set; }

        private void Awake()
        {
            var cellsSet = GameObject.FindGameObjectWithTag(Tags.Tiles);

            if (cellsSet == null)
            {
                throw new System.Exception("Tiles collection not found");
            }

            Tiles = cellsSet.GetComponentsInChildren<Tile>().ToList();

            if (Tiles.Count == 0)
            {
                throw new System.Exception("Empty tiles collection");
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
            var playerInitialPosition = Tiles.GetRandomElement();

            var playerToken = PlayerToken.CreatePlayerToken(playerId);
            PlayerTokens.Add(playerToken);

            playerToken.PlaceAt(playerInitialPosition);
        }
    }
}
