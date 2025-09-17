using UnityEngine;

public class NewInputReader : IInputReader
{
    private readonly InputSystem_Actions _input;

    public NewInputReader(InputSystem_Actions input)
    {
        _input = input;
        _input.Enable();
    }

    // ReadValue New Input System
    public Vector3 ReadMovement()
    {
        Vector2 inputVec = _input.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVec.x, 0, inputVec.y);
    }
}
