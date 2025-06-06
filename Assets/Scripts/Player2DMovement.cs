using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;

    private InputHandler _playerMovementInput;
    private Rigidbody2D _playerRb;
    private PlayerState _playerState;
    private PlayerAnimation _playerAnimation;
    private PlayerEffects _playerEffects;

    private float _moveSpeed = 7f;
    private float _jumpForce = 25f;

    private bool _isGrounded;
    private bool _DealtDamage;

    private float _groundCheckRadius = 0.6f;



    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerState = GetComponent<PlayerState>();
        _playerMovementInput = GetComponent<InputHandler>();
        _playerRb = GetComponent<Rigidbody2D>();
        _playerEffects = GetComponent<PlayerEffects>();
    }
    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerRotation();
        HandlePlayerStates();
    }
    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint == null)
            return;

        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
    private void TryDealDamageUnderPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(_groundCheckPoint.position, 0.3f, _groundLayer);
        if (hit != null && hit.TryGetComponent<IHasHealth>(out IHasHealth target) && !_DealtDamage)
        {
            target.TakeDamage(1);
            _playerEffects.DisableTrail();
            _DealtDamage = true;
        }
    }

    private void HandlePlayerMovement()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundCheckPoint.position, _groundCheckRadius, Vector2.down, 0.1f, _groundLayer);
        _isGrounded = hit.collider != null && hit.normal.y > 0.5f;
        bool isInFallingState = _playerState.CurrentMovementState == PlayerMovementState.Falling;
        bool isInJumpingState = _playerState.CurrentMovementState == PlayerMovementState.Jumping;
     
        if (_playerMovementInput != null)
        {
            if (_playerMovementInput.JumpPressed && _isGrounded && !isInFallingState && !isInJumpingState)
            {

                SceneManager.Instance?.ChangeAllColorsInScene();
                _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _jumpForce);
                _playerMovementInput.SetJumpPressedFalse();
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
        bool isSpinning = _playerAnimation.IsSpinning;


        if (verticalVelocity > epsilon && !_isGrounded)
        {
            _DealtDamage = false;
            _playerState.SetCurrentMovementState(PlayerMovementState.Jumping);
            return;
        }
        else if (verticalVelocity < -epsilon && !_isGrounded && !isSpinning)
        {
            _DealtDamage = false;
            _playerState.SetCurrentMovementState(PlayerMovementState.Falling);
            _playerMovementInput.SetJumpPressedFalse();
            return;
        }
        else if (_isGrounded && (verticalVelocity < -epsilon || verticalVelocity > epsilon))
        {
            _playerState.SetCurrentMovementState(PlayerMovementState.RidingPlatform);
            return;
        }
        else if (_isGrounded && Mathf.Abs(verticalVelocity) <= epsilon)
        {
            _playerState.SetCurrentMovementState(PlayerMovementState.Idle);
            TryDealDamageUnderPlayer();
            return;
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
            if (platform != null)
            {
                Physics2D.IgnoreCollision(_playerCollider, platform, true);
            }
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var platform in platforms)
        {

            if (platform != null)
            {
                Physics2D.IgnoreCollision(_playerCollider, platform, true);
            }
        }
    }
}
