namespace Assets.Scripts.Commands
{
    public class MovePlayerCommand : BaseCommand
    {
        public long PlayerId { get; set; }

        public int TileId { get; set; }
    }
}
