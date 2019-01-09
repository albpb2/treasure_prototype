using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField]
        private Material _baseMaterial;
        [SerializeField]
        private Material _cavedMaterial;

        public Material BaseMaterial => _baseMaterial;

        public Material CavedMaterial => _cavedMaterial;
    }
}
