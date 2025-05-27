using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private PlayerMovementInput _playerMovementInput;
    private Rigidbody2D _playerRb;

    private float _moveSpeed = 5f;
    private float _jumpForce = 7f;

    private bool _isGrounded =true;

    private void Awake()
    {
        _playerMovementInput = GetComponent<PlayerMovementInput>(); 
        _playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        print(_playerMovementInput.JumpPressed);
        if (_playerMovementInput != null)
        {
            if (_playerMovementInput.JumpPressed && _isGrounded)
            {
                print("Jump");
                _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _jumpForce);
                //isJumpPressed = false;
            }
        }
        else { print("WTF?"); }
    }
}
