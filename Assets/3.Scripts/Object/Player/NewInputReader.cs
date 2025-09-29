using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputReader : IInputReader
{
    private readonly InputActionAsset _action;

    public NewInputReader(InputActionAsset action)
    {
        _action = action;
        _action.Enable();
    }

    // ReadValue New Input System
    public Vector3 ReadMovement()
    {
        Vector2 inputVec = _action["Move"].ReadValue<Vector2>();
        return new Vector3(inputVec.x, 0, inputVec.y);
    }

    public Vector2 ReadRotation()
    {
        Vector2 inputVec = _action["Look"].ReadValue<Vector2>();
        return inputVec;
    }

    public bool ReadJump()
    {
        return _action["Jump"].triggered;
    }

    public bool ReadLeftClick()
    {
        return _action["Attack"].triggered;
    }

    public bool ReadInteract()
    {
        return _action["Interact"].triggered;
    }

    public bool ReadTab()
    {
        return _action["Inventory"].triggered;
    }

    public bool ReadEscape()
    {
        return _action["Setting"].triggered;
    }

    public bool ReadBuild()
    {
        return _action["Place Mode"].triggered;
    }

    public bool ReadEdit()
    {
        return _action["Edit Mode"].triggered;
    }

    public bool ReadDelete()
    {
        return _action["Destruction Mode"].triggered;
    }

    public bool ReadRotate()
    {
        return _action["Rotate"].triggered;
    }

    public bool ReadCancel()
    {
        return _action["Cancel"].triggered;
    }
}
