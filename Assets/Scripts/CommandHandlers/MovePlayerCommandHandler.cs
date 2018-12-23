using Assets.Scripts.Commands;
using Assets.Scripts.Map;

namespace Assets.Scripts.CommandHandlers
{
    public class MovePlayerCommandHandler : CommandHandlerBase, ICommandHandler<MovePlayerCommand>
    {
        public void Execute(MovePlayerCommand command)
        {
            var playerToken = BoardManager.FindPlayerToken(command.PlayerId);
            var tile = BoardManager.FindTile(command.TileId);

            playerToken.MoveTo(tile);
        }
    }
}
