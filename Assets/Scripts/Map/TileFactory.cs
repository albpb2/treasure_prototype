using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class TileFactory
    {
        private GameObject _cellsOrigin;

        private Dictionary<TileType, string> _tilePrefabs = new Dictionary<TileType, string>
        {
            [TileType.Grass] = "GrassHexagon.prefab",
            [TileType.Sand] = "SandHexagon.prefab",
        };

        private System.Random _random = new System.Random();

        private string GetTilePath(TileType tileType)
        {
            const string ResourcesPath = "Models/Terrain/Materials";

            return ResourcesPath + "/" + _tilePrefabs[tileType];
        }

        private GameObject GetCellsOrigin()
        {
            if (_cellsOrigin == null)
            {
                _cellsOrigin = GameObject.Find("Tiles");

                if (_cellsOrigin == null)
                {
                    _cellsOrigin = new GameObject("Tiles");
                    _cellsOrigin.transform.position = new Vector3(0, 0, 0);
                    _cellsOrigin.tag = Tags.Tiles;
                }
            }

            return _cellsOrigin;
        }

        public void CreateTile(float x, float y, int id)
        {
            var tileTypeIndex = _random.Next(0, _tilePrefabs.Count);
            var tileType = (TileType)tileTypeIndex;
        }

        public Tile CreateTile(TileType tileType, float x, float y, int id)
        {
            var tileObject = GameObject.Instantiate(
                Resources.Load(GetTilePath(tileType), typeof(GameObject)),
                new Vector3(x, 0, y),
                Quaternion.Euler(90, 0, 0),
                GetCellsOrigin().transform) as GameObject;

            var tile = tileObject.AddComponent<Tile>();
            tileObject.AddComponent<MeshCollider>();
            tile.Id = id;
            tile.Type = tileType;

            return tile;
        }
    }
}
