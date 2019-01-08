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

        private TileFactory _tileFactory;
        private Random _random;
        
        public BoardGenerator(int width, int height)
        {
            _random = new Random();
            _tileFactory = new TileFactory();
            _width = width;
            _height = height;
        }

        public BoardGenerator()
        {
            _random = new Random();
            _tileFactory = new TileFactory();

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
            
            var firstColumnTileId = 0;

            for (var i = 0; i < _width; i++)
            {
                PaintColumn(x, y, firstColumnTileId);

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

        private void PaintColumn(float x, float startingY, int firstTileId)
        {
            var y = startingY;
            var tileId = firstTileId;

            for (var j = 0; j < _height; j++)
            {
                _tileFactory.CreateTile(x, y, tileId);

                y += RowsCenterDistances * 2f;
                tileId += 1;
            }
        }
    }
}
