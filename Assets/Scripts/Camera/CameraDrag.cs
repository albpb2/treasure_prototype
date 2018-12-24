using Assets.Scripts.Input;
using Assets.Scripts.Match;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [SerializeField]
    private float dragSpeed = 0.2f;

    private Space _movementSpace = Space.World;
    private InputManager _inputManager;
    private MatchManager _matchManager;

    public void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _matchManager = FindObjectOfType<MatchManager>();
    }

    public void Update()
    {
        if (_matchManager.Pause)
        {
            return;
        }

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