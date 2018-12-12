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
    [SerializeField]
    private int _width = 0;

    [SerializeField]
    private int _height = 0;

    [MenuItem("Tools/Generate tiles")]
    static void TileGeneratorWizard()
    {
        ScriptableWizard.DisplayWizard<TileGenerator>("Tile generator", "Generate");
    }

    void OnWizardCreate()
    {
        const float ColumnsCenterDistances = 1.73f;
        const float RowsCenterDistances = 1.5f;

        float x = 0;
        float y = 0;

        var hexagonPrefabsResourcePaths = GetHexagonsPrefabsResourcePaths().ToList();

        var random = new Random();

        for (var i = 0; i < _width; i ++)
        {
            // paint column
            for (var j = 0; j < _height; j++)
            {
                var prefabIndex = random.Next(0, hexagonPrefabsResourcePaths.Count);
                Instantiate(Resources.Load(hexagonPrefabsResourcePaths[prefabIndex], typeof(GameObject)), new Vector3(x, 0, y), Quaternion.Euler(90, 0, 0));
                
                y += RowsCenterDistances * 2f;
            }

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
}
