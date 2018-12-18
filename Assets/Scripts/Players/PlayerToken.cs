using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Match;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerToken : MonoBehaviour
    {
        private Shader _originalShader;
        private Shader _selectedShader;

        public bool Selected { get; private set; }

        private readonly List<Color> _colors = new List<Color>
        {
            Color.red,
            Color.blue,
            Color.black,
            Color.white,
            Color.green,
            Color.yellow
        };

        public static PlayerToken CreatePlayerToken()
        {
            var baseObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            baseObject.AddComponent<Collider>();

            var playerToken = baseObject.AddComponent<PlayerToken>();
            
            const float TokenScale = 0.5f;
            playerToken.transform.localScale *= TokenScale;

            playerToken.SetRandomColor();

            return playerToken;
        }

        public void MoveTo(Tile tile)
        {
            const float HexagonHeight = 0.367f;

            transform.position = tile.transform.position + new Vector3(0, HexagonHeight, 0);

            tile.Uncover();

            ChangeSelection();

            var matchManager = FindObjectOfType<MatchManager>();

            matchManager.CurrentTile = tile;
        }
        
        private void SetRandomColor()
        {
            var materialColored = new Material(Shader.Find("Diffuse"));
            materialColored.color = _colors.GetRandomElement();
            GetComponent<Renderer>().material = materialColored;
        }

        private void Awake()
        {
            _originalShader = GetComponent<Renderer>().material.shader;
            _selectedShader = Shader.Find("Hidden/SceneViewSelected");
        }

        private void OnMouseDown()
        {
            ChangeSelection();
        }

        private void UpdateShader()
        {
            var renderer = GetComponent<Renderer>();
            if (!Selected)
            {
                renderer.material.shader = _originalShader;
            }
            else
            {
                renderer.material.shader = _selectedShader;
            }
        }

        private void ChangeSelection()
        {
            Selected = !Selected;
            UpdateShader();
        }
    }
}
