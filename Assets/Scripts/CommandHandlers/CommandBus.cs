using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CommandHandlers
{
    public class CommandBus : MonoBehaviour
    {
        private BoardManager _boardManager;
        private Dictionary<Type, CommandHandlerBase> _commandHandlers;

        public List<BaseCommand> TurnCommands { get; set; }

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();

            _commandHandlers = new Dictionary<Type, CommandHandlerBase>();
            
            TurnCommands = new List<BaseCommand>();
        }

        public void Start()
        {
            _commandHandlers.Add(typeof(MovePlayerCommand), new MovePlayerCommandHandler { BoardManager = _boardManager });
        }

        public void ExecuteInThisTurn<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            TurnCommands.Add(command);

            Execute(command);
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
    }
}
