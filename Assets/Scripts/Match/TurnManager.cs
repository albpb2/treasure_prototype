using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField]
        private TurnActionsPanel _turnActionsPanel;

        private BoardManager _boardManager;
        private MatchManager _matchManager;

        public bool HasTurnBeenPlayed { get; private set; }

        public delegate void TurnReset();
        public static event TurnReset onTurnReset;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchManager = FindObjectOfType<MatchManager>();

            Tile.onTileClicked += ShowActionsPanel;
        }

        public void EndTurn()
        {
            HasTurnBeenPlayed = false;

            _matchManager.SwitchCurrentPlayer();

            onTurnReset();

            _matchManager.SaveStatus();
        }

        public void PlayTurn()
        {
            HasTurnBeenPlayed = true;
        }

        private void ShowActionsPanel(Tile tile)
        {
            _matchManager.Pause = true;
            _turnActionsPanel.gameObject.SetActive(true);
        }
    }
}
