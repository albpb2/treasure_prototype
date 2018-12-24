using Assets.Scripts.Map;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
        var boardGenerator = new BoardGenerator(_width, _height);
        boardGenerator.PaintBoard();
    }

    void OnWizardUpdate()
    {
        helpString = "Make width greater than height";
    }
}
