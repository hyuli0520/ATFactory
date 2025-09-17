using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private IMovement _movement;
    private IInputReader _inputReader;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();
        var input = new InputSystem_Actions();

        _inputReader = new NewInputReader(input);
        _movement = new PlayerMovement(controller, moveSpeed);
    }

    private void Update()
    {
        Vector3 inputDir = _inputReader.ReadMovement();
        Vector3 moveDir = Camera.main.transform.TransformDirection(inputDir);
        moveDir.y = 0;

        _movement.Move(moveDir);
    }
}
