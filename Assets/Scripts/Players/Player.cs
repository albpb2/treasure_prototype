namespace Assets.Scripts.Players
{
    public class Player
    {
        public Player(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public int Gold { get; set; } = 0;
    }
}
