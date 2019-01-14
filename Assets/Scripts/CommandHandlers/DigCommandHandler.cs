using Assets.Scripts.Commands;

namespace Assets.Scripts.CommandHandlers
{
    public class DigCommandHandler : CommandHandlerBase, ICommandHandler<DigCommand>
    {
        public void Execute(DigCommand command)
        {
            var tile = BoardManager.FindTile(command.TileId);

            tile.Dig();

            CreditGoldToPlayer(MatchManager.GoldPerDig);
        }
    }
}
