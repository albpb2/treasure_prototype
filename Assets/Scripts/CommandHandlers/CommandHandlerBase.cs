using Assets.Scripts.Map;
using Assets.Scripts.Match;
using Assets.Scripts.Players;

namespace Assets.Scripts.CommandHandlers
{
    public class CommandHandlerBase
    {
        public BoardManager BoardManager { get; set; }

        public MatchManager MatchManager{ get; set; }

        public PlayerInfoPanel PlayerInfoPanel { get; set; }

        protected void CreditGoldToPlayer(int gold)
        {
            var player = MatchManager.CurrentPlayer;
            player.Gold += gold;
            PlayerInfoPanel.SetPlayerInfo(MatchManager.CurrentPlayer, MatchManager.CurrentPlayerToken);
        }

        protected void ReceiveGoldFromPlayer(int gold)
        {
            var player = MatchManager.CurrentPlayer;
            player.Gold -= gold;
            PlayerInfoPanel.SetPlayerInfo(MatchManager.CurrentPlayer, MatchManager.CurrentPlayerToken);
        }
    }
}
