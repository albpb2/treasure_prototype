using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {
        public Vector3 Center { get; set; }

        public bool IsUncovered { get; private set; }

        private void Awake()
        {
            IsUncovered = false;
        }

        public void Uncover()
        {
            transform.Rotate(Vector3.down, 180);

            IsUncovered = true;
        }
    }
}
