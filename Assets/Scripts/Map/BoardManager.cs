using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class BoardManager : MonoBehaviour
    {
        public List<Tile> Tiles { get; private set; }

        private void Awake()
        {
            var cellsSet = GameObject.FindGameObjectWithTag(Tags.Tiles);

            if (cellsSet == null)
            {
                throw new System.Exception("Tiles collection not found");
            }

            Tiles = cellsSet.GetComponentsInChildren<Tile>().ToList();

            if (Tiles.Count == 0)
            {
                throw new System.Exception("Empty tiles collection");
            }
        }
    }
}
