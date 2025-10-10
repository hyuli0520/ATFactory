using UnityEngine;

/// <summary>
/// Player Input Interface
/// </summary>
public interface IInputReader
{
    Vector3 ReadMovement();
    Vector2 ReadRotation();
    bool ReadJump();
    bool ReadLeftClick();
    bool ReadInteract();
    bool ReadTab();
    bool ReadEscape();
    bool ReadBuild();
    bool ReadEdit();
    bool ReadDelete();
    bool ReadRotate();
    bool ReadCancel();
}
