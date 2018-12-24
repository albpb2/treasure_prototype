using Assets.Scripts.Match;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class PlayerBoardCamera : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        private MatchManager _matchManager;

        public void Awake()
        {
            _camera = GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();
            _matchManager = FindObjectOfType<MatchManager>();
            MatchManager.onCurrentPlayerChanged += AimAtCurrentPlayerToken;
        }

        public void AimAtPlayerToken(PlayerToken playerToken)
        {
            const float DistanceToPlayerX = 0.3f;
            const float DistanceToPlayerY = 4.16f;
            const float DistanceToPlayerZ = -1.6f;

            var x = playerToken.transform.position.x + DistanceToPlayerX;
            var y = playerToken.transform.position.y + DistanceToPlayerY;
            var z = playerToken.transform.position.z + DistanceToPlayerZ;

            _camera.transform.position = new Vector3(x, y, z);
        }

        public void AimAtCurrentPlayerToken()
        {
            AimAtPlayerToken(_matchManager.CurrentPlayerToken);
        }
    }
}
