using UnityEngine;

public class PlayerMovement : IMovement
{
    private CharacterController _controller;
    private float _speed;

    public PlayerMovement(CharacterController controller, float speed)
    {
        _controller = controller;
        _speed = speed;
    }

    public void Move(Vector3 direction)
    {
        Vector3 move = direction * _speed;
        _controller.Move(move * Time.deltaTime);
    }
}
