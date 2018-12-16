using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class MatchManager : MonoBehaviour
    {
        private BoardManager _boardManager;

        void Awake()
        {
            _boardManager = GameObject.FindObjectOfType<BoardManager>();
        }

        private void Start()
        {
            var playerInitialPosition = _boardManager.Tiles.GetRandomElement();

            var playerToken = GameObject.CreatePrimitive(PrimitiveType.Cube);
            playerToken.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            playerToken.transform.position = playerInitialPosition.transform.position + new Vector3(0, 0.25f, 0);
        }
    }
}
