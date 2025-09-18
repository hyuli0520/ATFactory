using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private IMovement _movement;
    private IInputReader _inputReader;
    private ILook _look;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();
        var input = new InputSystem_Actions();

        _inputReader = new NewInputReader(input);
        _movement = new PlayerMovement(controller, moveSpeed, gravity, jumpHeight);
        _look = new FPSLook(Camera.main.transform, transform, mouseSensitivity);
    }

    private void Update()
    {
        Vector3 inputDir = _inputReader.ReadMovement();
        Vector2 inputRotation = _inputReader.ReadRotation();

        Vector3 moveDir = Camera.main.transform.TransformDirection(inputDir);
        moveDir.y = 0;

        _movement.Move(moveDir);
        _look.Look(inputRotation);

        if (_inputReader.ReadJump())
            _movement.Jump();
    }
}
