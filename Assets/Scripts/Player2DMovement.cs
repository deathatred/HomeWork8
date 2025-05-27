using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;

    private PlayerMovementInput _playerMovementInput;
    private Rigidbody2D _playerRb;

    private float _moveSpeed = 7f;
    private float _jumpForce = 20f;

    private bool _isGrounded;
    private float _groundCheckDistance = 0.2f;
    private float _groundCheckRadius = 0.2f;

    private bool isInJumpingState;
    private bool isInFallingState;
    private bool isInIdleState;

    private void Awake()
    {
        _playerMovementInput = GetComponent<PlayerMovementInput>();
        _playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerStates();
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHasHealth>(out IHasHealth other))
        {
            print("collide");
            if (!isInJumpingState)
            {
                other.TakeDamage(1);
                print("Dealt damage");
            }
        }
    }

    private void HandlePlayerMovement()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        if (_playerMovementInput != null)
        {
            if (_playerMovementInput.JumpPressed && _isGrounded && !isInFallingState)
            {
                print("Jump");
                _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _jumpForce);
            }
        }
        else { print("Player Input is Null"); }

        _playerRb.linearVelocity = new Vector2(_playerMovementInput.MoveInput.x * _moveSpeed, _playerRb.linearVelocity.y);
    }
    private void HandlePlayerStates()
    {
        if (_playerRb.linearVelocity.y > 0)
        {
            isInJumpingState = true;
            isInFallingState = false;
            isInIdleState = false;
        }
        else if (_playerRb.linearVelocity.y < 0)
        {
            isInFallingState = true;
            isInJumpingState = false;
            isInIdleState = false;
        }
        else if (_playerRb.linearVelocity.y == 0)
        {
            isInIdleState = true;
            isInFallingState = false;
            isInJumpingState = false;
        }

    }
    }
