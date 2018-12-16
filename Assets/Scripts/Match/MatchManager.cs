using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class MatchManager : MonoBehaviour
    {
        private const float TokenScale = 0.5f;

        private readonly List<Color> _colors = new List<Color>
        {
            Color.red,
            Color.blue,
            Color.black,
            Color.white,
            Color.green,
            Color.yellow
        };

        private BoardManager _boardManager;

        void Awake()
        {
            _boardManager = GameObject.FindObjectOfType<BoardManager>();
        }

        private void Start()
        {
            PlacePlayerToken(CreatePlayerToken());
        }

        private GameObject CreatePlayerToken()
        {
            var playerToken = GameObject.CreatePrimitive(PrimitiveType.Cube);

            ResizePlayerToken(playerToken);

            ColorPlayerToken(playerToken);

            return playerToken;
        }

        private void ResizePlayerToken(GameObject playerToken)
        {
            playerToken.transform.localScale *= TokenScale;
        }

        private void ColorPlayerToken(GameObject playerToken)
        {
            var materialColored = new Material(Shader.Find("Diffuse"));
            materialColored.color = _colors.GetRandomElement();
            playerToken.GetComponent<Renderer>().material = materialColored;
        }

        private void PlacePlayerToken(GameObject playerToken)
        {
            var playerInitialPosition = _boardManager.Tiles.GetRandomElement();

            const float HexagonHeight = 0.367f;

            playerToken.transform.position = playerInitialPosition.transform.position +
                new Vector3(0, HexagonHeight, 0);
        }
    }
}
