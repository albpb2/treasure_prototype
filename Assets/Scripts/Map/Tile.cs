using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {
        private Shader _originalShader;
        private Shader _selectedShader;
        private Renderer _renderer;

        public Vector3 Center { get; set; }

        public bool IsUncovered { get; private set; }

        private void Awake()
        {
            IsUncovered = false;
            _originalShader = GetComponent<Renderer>().material.shader;
            _selectedShader = Shader.Find("Outlined/Silhouetted Diffuse");
            _renderer = GetComponent<Renderer>();
        }

        public void Uncover()
        {
            transform.Rotate(Vector3.down, 180);

            IsUncovered = true;
        }
        
        private void OnMouseOver()
        {
            _renderer.material.shader = _selectedShader;
        }

        private void OnMouseExit()
        {
            _renderer.material.shader = _originalShader;
        }
    }
}
