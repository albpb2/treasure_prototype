using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match.TurnActionsButtons
{
    public abstract class TurnActionsButton : MonoBehaviour
    {
        public abstract bool ShouldBeEnabled(Tile tile);
    }
}
