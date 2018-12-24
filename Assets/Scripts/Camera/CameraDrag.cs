using Assets.Scripts.Input;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [SerializeField]
    private float dragSpeed = 0.2f;

    private Space _movementSpace = Space.World;
    private InputManager _inputManager;

    public void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
    }

    public void Update()
    {
        if (_inputManager.RightPressed)
        {
            transform.Translate(Vector3.right * dragSpeed, _movementSpace);
        }
        if (_inputManager.LeftPressed)
        {
            transform.Translate(Vector3.left * dragSpeed, _movementSpace);
        }
        if (_inputManager.UpPressed)
        {
            transform.Translate(Vector3.forward * dragSpeed, _movementSpace);
        }
        if (_inputManager.DownPressed)
        {
            transform.Translate(Vector3.back * dragSpeed, _movementSpace);
        }
    }
}