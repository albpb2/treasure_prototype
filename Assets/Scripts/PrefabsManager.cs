using UnityEngine;

namespace Assets.Scripts
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _farmPrefab;

        public GameObject FarmPrefab => _farmPrefab;
    }
}
