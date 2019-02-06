using Assets.Scripts.Match;
using UnityEngine;

namespace Assets.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        private MatchManager _matchManager;

        public void Awake()
        {
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void CreditGoldToPlayer(long playerId, int gold)
        {
            var player = _matchManager.FindPlayer(playerId);
            player.Gold += gold;

            if (_matchManager.CurrentPlayerId == playerId)
            {
                _matchManager.PlayerInfoPanel.SetPlayerInfo(
                    player,
                    _matchManager.CurrentPlayerToken);
            }
        }
    }
}
