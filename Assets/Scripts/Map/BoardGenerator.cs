using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Map
{
    public class BoardGenerator
    {
        private const int MaxWidth = 20;
        private const int MaxHeight = 10;
        private const int MinWidth = 10;
        private const int MinHeight = 5;

        private const float ColumnsCenterDistances = 1.73f;
        private const float RowsCenterDistances = 1.5f;

        private int _width = 0;
        private int _height = 0;

        private Random _random;
        private List<string> _hexagonPrefabsResourcePaths;
        
        public BoardGenerator(int width, int height)
        {
            _hexagonPrefabsResourcePaths = GetHexagonsPrefabsResourcePaths().ToList();
            _random = new Random();
            _width = width;
            _height = height;
        }

        public BoardGenerator()
        {
            _hexagonPrefabsResourcePaths = GetHexagonsPrefabsResourcePaths().ToList();
            _random = new Random();

            _height = _random.Next(MinHeight, MaxHeight);
            _width = _random.Next(MinWidth, MaxWidth);
            while (_width <= _height)
            {
                _width = _random.Next(MinWidth, MaxWidth);
            }
        }

        public void PaintBoard()
        {
            float x = 0;
            float y = 0;

            GameObject cellsOrigin = new GameObject("Tiles");
            cellsOrigin.transform.position = new Vector3(0, 0, 0);
            cellsOrigin.tag = Tags.Tiles;
            var firstColumnTileId = 0;

            for (var i = 0; i < _width; i++)
            {
                PaintColumn(x, y, cellsOrigin, firstColumnTileId);

                if (i % 2 == 0)
                {
                    y = RowsCenterDistances;
                }
                else
                {
                    y = 0;
                }
                x += ColumnsCenterDistances / 2f;
                firstColumnTileId += _height;
            }

            var a = 0;
        }

        private void DeleteCurrentTiles()
        {
            var tiles = GameObject.FindObjectsOfType<Tile>();

            foreach (var tile in tiles)
            {
                Object.Destroy(tile);
            }
        }

        private IEnumerable<string> GetHexagonsPrefabsResourcePaths()
        {
            const string ResourcesPath = "Models/Terrain/Materials";

            var assetsPath = Application.dataPath + "/Resources/" + ResourcesPath;

            var materialsList = Directory.GetFiles(assetsPath);

            var hexagonPrefabFilePaths = materialsList.Where(f => f.EndsWith("Hexagon.prefab"));

            var hexagonPrefabsFileNames = hexagonPrefabFilePaths.Select(f => Path.GetFileName(f).Replace(".prefab", ""));

            return hexagonPrefabsFileNames.Select(fileName => ResourcesPath + "/" + fileName);
        }

        private void PaintColumn(float x, float startingY, GameObject cellsOrigin, int firstTileId)
        {
            var y = startingY;
            var tileId = firstTileId;

            for (var j = 0; j < _height; j++)
            {
                PaintTile(x, y, cellsOrigin, tileId);

                y += RowsCenterDistances * 2f;
                tileId += 1;
            }
        }

        private void PaintTile(float x, float y, GameObject cellsOrigin, int id)
        {
            var prefabIndex = _random.Next(0, _hexagonPrefabsResourcePaths.Count);

            var tileObject = GameObject.Instantiate(
                Resources.Load(_hexagonPrefabsResourcePaths[prefabIndex], typeof(GameObject)),
                new Vector3(x, 0, y),
                Quaternion.Euler(90, 0, 0),
                cellsOrigin.transform) as GameObject;

            var tile = tileObject.AddComponent<Tile>();
            tileObject.AddComponent<MeshCollider>();
            tile.Id = id;
        }
    }
}
