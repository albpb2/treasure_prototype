using Assets.Scripts.Match;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {
        public const float MaxCenterToCenterDistance = 1.73f;

        private Shader _originalShader;
        private Shader _selectedShader;
        private Renderer _renderer;

        public bool IsUncovered { get; private set; }

        public int Id { get; set; }

        private void Awake()
        {
            IsUncovered = false;
            _originalShader = GetComponent<Renderer>().material.shader;
            _selectedShader = Shader.Find("Outlined/Silhouetted Diffuse");
            _renderer = GetComponent<Renderer>();
        }

        public void Uncover()
        {
            if (!IsUncovered)
            {
                transform.Rotate(Vector3.down, 180);

                IsUncovered = true;
            }
        }
        
        private void OnMouseOver()
        {
            _renderer.material.shader = _selectedShader;
        }

        private void OnMouseExit()
        {
            _renderer.material.shader = _originalShader;
        }

        private void OnMouseDown()
        {
            var matchManager = FindObjectOfType<MatchManager>();

            if (matchManager.PlayerToken.Selected && IsAdjacentTo(matchManager.CurrentTile))
            {
                matchManager.PlayerToken.MoveTo(this);
            }
        }

        private bool IsAdjacentTo(Tile tile)
        {
            const float AdjacencyDistanceMargin = 0.2f;
            return Vector3.Distance(transform.position, tile.transform.position) < MaxCenterToCenterDistance * (1 + AdjacencyDistanceMargin);
        }
    }
}
