using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEditor;
using Random = System.Random;

public class TileGenerator : UnityEditor.ScriptableWizard
{
    private const float ColumnsCenterDistances = 1.73f;
    private const float RowsCenterDistances = 1.5f;

    [SerializeField]
    private int _width = 0;

    [SerializeField]
    private int _height = 0;

    private Random _random;
    private List<string> _hexagonPrefabsResourcePaths;

    [MenuItem("Tools/Generate tiles")]
    static void TileGeneratorWizard()
    {
        ScriptableWizard.DisplayWizard<TileGenerator>("Tile generator", "Generate");
    }

    void OnWizardCreate()
    {
        float x = 0;
        float y = 0;

        _hexagonPrefabsResourcePaths = GetHexagonsPrefabsResourcePaths().ToList();
        _random = new Random();

        for (var i = 0; i < _width; i ++)
        {
            PaintColumn(x, y);

            if (i % 2 == 0)
            {
                y = RowsCenterDistances;
            }
            else
            {
                y = 0;
            }
            x += ColumnsCenterDistances / 2f;
        }

        var a = 0;
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

    private void PaintColumn(float x, float startingY)
    {
        var y = startingY;

        for (var j = 0; j < _height; j++)
        {
            PaintCell(x, y);

            y += RowsCenterDistances * 2f;
        }
    }

    private void PaintCell(float x, float y)
    {
        var prefabIndex = _random.Next(0, _hexagonPrefabsResourcePaths.Count);
        Instantiate(
            Resources.Load(_hexagonPrefabsResourcePaths[prefabIndex], typeof(GameObject)),
            new Vector3(x, 0, y),
            Quaternion.Euler(90, 0, 0));
    }
}
