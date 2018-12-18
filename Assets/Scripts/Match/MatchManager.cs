using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class MatchManager : MonoBehaviour
    {
        private BoardManager _boardManager;

        public PlayerToken PlayerToken { get; set; }

        public Tile CurrentTile { get; set; }

        void Awake()
        {
            _boardManager = GameObject.FindObjectOfType<BoardManager>();
        }

        private void Start()
        {
            var playerInitialPosition = _boardManager.Tiles.GetRandomElement();

            PlayerToken = PlayerToken.CreatePlayerToken();

            PlayerToken.MoveTo(playerInitialPosition);
        }
    }
}