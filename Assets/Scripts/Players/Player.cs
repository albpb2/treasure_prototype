using Assets.Scripts.Map.Locations;

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

        public Farm Farms { get; set; }
    }
}
