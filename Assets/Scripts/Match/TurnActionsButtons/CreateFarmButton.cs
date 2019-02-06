using Assets.Scripts.Map;

namespace Assets.Scripts.Match.TurnActionsButtons
{
    public class CreateFarmButton : TurnActionsButton
    {
        public override bool ShouldBeEnabled(Tile tile)
        {
            return tile.IsUncovered && !tile.IsExploited && _matchManager.CurrentPlayer.Gold >= _matchManager.FarmCost;
        }
    }
}
