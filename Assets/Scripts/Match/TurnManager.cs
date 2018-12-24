using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnManager : MonoBehaviour
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;

        public bool HasTurnBeenPlayed { get; private set; }

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void EndTurn()
        {
            foreach(var playerToken in _boardManager.PlayerTokens)
            {
                if (playerToken.Selected)
                {
                    playerToken.ChangeSelection();
                }
            }

            _matchManager.SwitchCurrentPlayer();

            HasTurnBeenPlayed = false;
        }

        public void PlayTurn()
        {
            HasTurnBeenPlayed = true;
        }
    }
}
