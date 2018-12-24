using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using Assets.Scripts.Match;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerToken : MonoBehaviour
    {
        private Shader _originalShader;
        private Shader _selectedShader;

        public bool Selected { get; private set; }

        public long PlayerId { get; set; }

        public Tile Tile { get; set; }

        private readonly List<Color> _colors = new List<Color>
        {
            Color.red,
            Color.blue,
            Color.black,
            Color.white,
            Color.green,
            Color.yellow
        };

        public static PlayerToken CreatePlayerToken(long playerId)
        {
            var baseObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            baseObject.AddComponent<Collider>();

            var playerToken = baseObject.AddComponent<PlayerToken>();
            
            const float TokenScale = 0.5f;
            playerToken.transform.localScale *= TokenScale;

            playerToken.SetRandomColor();

            playerToken.PlayerId = playerId;

            return playerToken;
        }

        public void MoveTo(Tile tile)
        {
            PlaceAt(tile);

            ChangeSelection();
        }

        public void PlaceAt(Tile tile)
        {
            const float HexagonHeight = 0.367f;

            transform.position = tile.transform.position + new Vector3(0, HexagonHeight, 0);

            tile.Uncover();

            if (Tile != null)
            {
                Tile.PlayerToken = null;
            }

            Tile = tile;

            Tile.PlayerToken = this;
        }

        public void ChangeSelection()
        {
            Selected = !Selected;
            UpdateShader();
        }

        private void SetRandomColor()
        {
            var materialColored = new Material(Shader.Find("Diffuse"));
            materialColored.color = GetRandomColor();
            GetComponent<Renderer>().material = materialColored;
        }

        private void Awake()
        {
            _originalShader = GetComponent<Renderer>().material.shader;
            _selectedShader = Shader.Find("Hidden/SceneViewSelected");

            PlayerId = 0; // to-do: change to real id
        }

        private void OnMouseDown()
        {
            var matchManager = FindObjectOfType<MatchManager>();

            if (matchManager.CurrentPlayerId == PlayerId)
            {
                ChangeSelection();
            }
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

        private Color GetRandomColor()
        {
            var usedColors = GetUsedColors().ToList();

            var color = _colors.GetRandomElement();

            while (usedColors.Contains(color))
            {
                color = _colors.GetRandomElement();
            }

            return color;
        }

        private IEnumerable<Color> GetUsedColors()
        {
            var tokens = FindObjectsOfType<PlayerToken>();

            return tokens.Select(token => token.GetComponent<Renderer>().material.color);
        }
    }
}
