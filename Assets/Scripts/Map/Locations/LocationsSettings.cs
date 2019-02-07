using UnityEngine;

namespace Assets.Scripts.Map.Locations
{
    public class LocationsSettings : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _farmLocalPosition;

        public Vector3 FarmLocalPosition => _farmLocalPosition;
    }
}
