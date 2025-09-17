using UnityEngine;

public class PlayerMovement : IMovement
{
    private CharacterController _controller;
    private float _speed;
    private float _gravity;

    private Vector3 _velocity;

    public PlayerMovement(CharacterController controller, float speed, float gravity)
    {
        _controller = controller;
        _speed = speed;
        _gravity = gravity;
    }

    public void Move(Vector3 direction)
    {
        Vector3 move = direction * _speed;
        _controller.Move(move * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y -= 2f;

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
