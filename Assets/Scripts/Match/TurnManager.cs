using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnManager : MonoBehaviour
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;

        public bool HasTurnBeenPlayed { get; private set; }

        public delegate void TurnReset();
        public static event TurnReset onTurnReset;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void EndTurn()
        {
            HasTurnBeenPlayed = false;

            _matchManager.SwitchCurrentPlayer();

            onTurnReset();
        }

        public void PlayTurn()
        {
            HasTurnBeenPlayed = true;
        }
    }
}
