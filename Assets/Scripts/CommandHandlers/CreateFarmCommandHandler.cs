using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using Assets.Scripts.Map.Locations;

namespace Assets.Scripts.CommandHandlers
{
    public class CreateFarmCommandHandler : CommandHandlerBase, ICommandHandler<CreateFarmCommand>
    {
        public void Execute(CreateFarmCommand command)
        {
            var tile = BoardManager.FindTile(command.TileId);
            
            var playerToken = BoardManager.FindPlayerToken(command.PlayerId);
            
            Farm.Instantiate(tile, playerToken);

            ReceiveGoldFromPlayer(MatchManager.FarmCost);
        }
    }
}
