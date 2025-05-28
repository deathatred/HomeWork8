using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;

    private PlayerMovementInput _playerMovementInput;
    private Rigidbody2D _playerRb;
    private PlayerState _playerState;

    private float _moveSpeed = 7f;
    private float _jumpForce = 20f;

    private bool _isGrounded;
    private float _groundCheckDistance = 0.2f;
    private float _groundCheckRadius = 0.5f;



    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
        _playerMovementInput = GetComponent<PlayerMovementInput>();
        _playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerRotation();
        HandlePlayerStates();
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        bool isInJumpingState = _playerState.CurrentMovementState == PlayerMovementState.Jumping;
        if (collision.gameObject.TryGetComponent<IHasHealth>(out IHasHealth other))
        {
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
        bool isInFallingState = _playerState.CurrentMovementState == PlayerMovementState.Falling;
        if (_playerMovementInput != null)
        {
            if (_playerMovementInput.JumpPressed && _isGrounded && !isInFallingState)
            {

                _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _jumpForce);
            }
        }
        else { print("Player Input is Null"); }

        if (_playerMovementInput.MoveInput.y < -0.5f && _isGrounded)
        {
            StartCoroutine(DisableCollisionTemporarily());
        }
        _playerRb.linearVelocity = new Vector2(_playerMovementInput.MoveInput.x * _moveSpeed, _playerRb.linearVelocity.y);
    }
    private void HandlePlayerStates()
    {
        float verticalVelocity = _playerRb.linearVelocity.y;
        float epsilon = 0.01f;

        if (verticalVelocity > epsilon)
        {
            _playerState.SetCurrentMovementState(PlayerMovementState.Jumping);
        }
        else if (verticalVelocity < -epsilon && !_isGrounded)
        {
            _playerState.SetCurrentMovementState(PlayerMovementState.Falling);
        }
        else
        {
            _playerState.SetCurrentMovementState(PlayerMovementState.Idle);
        }
    }
    private void HandlePlayerRotation()
    {
        float playerXScale = 5.15f;
        Vector2 moveInput = _playerMovementInput.MoveInput; 
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(playerXScale, transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-playerXScale, transform.localScale.y, transform.localScale.z);
        }
    }
    private IEnumerator DisableCollisionTemporarily()
    {
        Collider2D[] platforms = Physics2D.OverlapCircleAll(transform.position, 1f, _groundLayer);

        foreach (var platform in platforms)
        {
            Physics2D.IgnoreCollision(_playerCollider, platform, true);
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var platform in platforms)
        {
           
            Physics2D.IgnoreCollision(_playerCollider, platform, false);
        }
    }
}
