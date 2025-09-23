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
    bool ReadTab();
    bool ReadBuild();
    bool ReadEdit();
    bool ReadDelete();
    bool ReadRotate();
    bool ReadCancel();
}
