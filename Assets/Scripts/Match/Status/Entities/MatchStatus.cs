using System;
using System.Collections.Generic;

namespace Assets.Scripts.Match.Status.Entities.Match
{
    [Serializable]
    public class MatchStatus
    {
        public List<Player> Players;
        public List<Tile> Tiles;
        public List<PlayerToken> PlayerTokens;
        public long CurrentPlayer;
    }
}
