using Assets.Scripts.CommandHandlers;
using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnActionsPanel : MonoBehaviour
    {
        private MatchManager _matchManager;
        private CommandBus _commandBus;
        private TurnManager _turnManager;

        public void Awake()
        {
            _matchManager = FindObjectOfType<MatchManager>();
            _commandBus = FindObjectOfType<CommandBus>();
            _turnManager = FindObjectOfType<TurnManager>();
        }

        public Tile Tile { get; set; }

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
