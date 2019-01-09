using Assets.Scripts.Commands;

namespace Assets.Scripts.CommandHandlers
{
    public class MovePlayerCommandHandler : CommandHandlerBase, ICommandHandler<MovePlayerCommand>
    {
        public void Execute(MovePlayerCommand command)
        {
            MovePlayerToken(command.PlayerId, command.TileId);
            CreditGoldToPlayer();
        }

        private void MovePlayerToken(long playerId, int tileId)
        {
            var playerToken = BoardManager.FindPlayerToken(playerId);
            var tile = BoardManager.FindTile(tileId);
            playerToken.MoveTo(tile);
        }

        private void CreditGoldToPlayer()
        {
            var player = MatchManager.CurrentPlayer;
            player.Gold += MatchManager.GoldPerMovement;
            PlayerInfoPanel.SetPlayerInfo(MatchManager.CurrentPlayer, MatchManager.CurrentPlayerToken);
        }
    }
}
