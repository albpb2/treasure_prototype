using Assets.Scripts.Map;
using System;
using UnityEngine;

namespace Assets.Scripts.Match.Status.Entities.Match
{
    [Serializable]
    public class Tile
    {
        public int Id;
        public Vector3 Position;
        public bool Uncovered;
        public TileType TileType;
    }
}
