using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match.TurnActionsButtons
{
    public abstract class TurnActionsButton : MonoBehaviour
    {
        protected MatchManager _matchManager;

        public void Awake()
        {
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public abstract bool ShouldBeEnabled(Tile tile);
    }
}
