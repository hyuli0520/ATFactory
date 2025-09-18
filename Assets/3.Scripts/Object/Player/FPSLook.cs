using UnityEngine;

public class FPSLook : ILook
{
    private Transform cameraTransform;
    public Transform playerBody;
    public float mouseSensitivity;
    private float xRotation = 0f;

    public FPSLook(Transform cameraTransform, Transform playerBody, float mouse)
    {
        this.cameraTransform = cameraTransform;
        this.playerBody = playerBody;
        mouseSensitivity = mouse;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(Vector2 delta)
    {
        float mouseX = delta.x * mouseSensitivity;
        float mouseY = delta.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
