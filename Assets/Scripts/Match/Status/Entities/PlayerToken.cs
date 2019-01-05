using System;
using UnityEngine;

namespace Assets.Scripts.Match.Status.Entities.Match
{
    [Serializable]
    public class PlayerToken
    {
        public long PlayerId;
        public int TileId;
        public Color Color;
    }
}
