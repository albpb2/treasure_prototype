using UnityEngine;
using UnityInput = UnityEngine.Input;

namespace Assets.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public bool UpPressed
        {
            get
            {
                return UnityInput.GetKey(KeyCode.UpArrow) || UnityInput.GetKey(KeyCode.W);
            }
        }

        public bool DownPressed
        {
            get
            {
                return UnityInput.GetKey(KeyCode.DownArrow) || UnityInput.GetKey(KeyCode.S);
            }
        }

        public bool LeftPressed
        {
            get
            {
                return UnityInput.GetKey(KeyCode.LeftArrow) || UnityInput.GetKey(KeyCode.A);
            }
        }

        public bool RightPressed
        {
            get
            {
                return UnityInput.GetKey(KeyCode.RightArrow) || UnityInput.GetKey(KeyCode.D);
            }
        }
    }
}
