using Assets.Scripts.CommandHandlers;
using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Match
{
    public class TurnActionsPanel : MonoBehaviour, IDeselectHandler
    {
        private MatchManager _matchManager;
        private CommandBus _commandBus;
        private TurnManager _turnManager;

        public Tile Tile { get; set; }

        public void Awake()
        {
            _matchManager = FindObjectOfType<MatchManager>();
            _commandBus = FindObjectOfType<CommandBus>();
            _turnManager = FindObjectOfType<TurnManager>();
        }

        public void OnDeselect(BaseEventData data)
        {
            Debug.Log("Deselected");
        }

        public void Enable(Tile tile)
        {
            gameObject.SetActive(true);
            Tile = tile;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Dig()
        {
            _commandBus.ExecuteInThisTurn(new DigCommand
            {
                PlayerId = _matchManager.CurrentPlayerId,
                TileId = Tile.Id
            });
            _turnManager.HideActionsPanel();
        }
    }
}
