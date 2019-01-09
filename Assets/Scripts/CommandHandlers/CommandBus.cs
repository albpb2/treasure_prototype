using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using Assets.Scripts.Match;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CommandHandlers
{
    public class CommandBus : MonoBehaviour
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;
        private Dictionary<Type, CommandHandlerBase> _commandHandlers;
        private TurnManager _turnManager;

        public List<BaseCommand> TurnCommands { get; set; }

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchManager = FindObjectOfType<MatchManager>();
            _turnManager = FindObjectOfType<TurnManager>();

            _commandHandlers = new Dictionary<Type, CommandHandlerBase>();
            
            TurnCommands = new List<BaseCommand>();

            TurnManager.onTurnReset += ResetTurnCommands;
        }

        public void Start()
        {
            _commandHandlers.Add(typeof(MovePlayerCommand), new MovePlayerCommandHandler());

            foreach (var commandHandlerBase in _commandHandlers.Values)
            {
                commandHandlerBase.BoardManager = _boardManager;
                commandHandlerBase.MatchManager = _matchManager;
                commandHandlerBase.PlayerInfoPanel = _matchManager.PlayerInfoPanel;
            }
        }

        public void ExecuteInThisTurn<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            if (!_turnManager.HasTurnBeenPlayed && command.PlayerId == _matchManager.CurrentPlayerId)
            {
                TurnCommands.Add(command);

                Execute(command);

                _turnManager.PlayTurn();
            }
        }

        public void ExecuteFromPreviousTurn<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            Execute(command);
        }

        private void Execute<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            if (!_commandHandlers.ContainsKey(command.GetType()))
            {
                return;
            }

            var commandHandler = (ICommandHandler<TCommand>)_commandHandlers[command.GetType()];

            commandHandler.Execute(command);
        }

        private void ResetTurnCommands()
        {
            TurnCommands = new List<BaseCommand>();
        }
    }
}
