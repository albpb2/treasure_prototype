using Assets.Scripts.Map;

namespace Assets.Scripts.Match.TurnActionsButtons
{
    public class DigButton : TurnActionsButton
    {
        public override bool ShouldBeEnabled(Tile tile)
        {
            return tile.IsUncovered && !tile.IsExploited;
        }
    }
}
