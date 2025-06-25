using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player2DMovement : MonoBehaviour
{
    public static Player2DMovement Instance { get; private set; }

    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;

    private InputHandler _playerMovementInput;
    private Rigidbody2D _playerRb;
    private PlayerState _playerState;
    private PlayerAnimation _playerAnimation;
    private PlayerEffects _playerEffects;
    private Vector2 _playerVelocityCache;

    public event EventHandler OnFinishPlatformReached;

    private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 35f;

    private bool _isGrounded;
    private bool _DealtDamage;

    private float _groundCheckRadius = 0.6f;
    private float _ridingPlatformDelay = 0.15f;
    private float _ridingPlatformTimer = 0f;

    private bool _isKnockback;
    private float _knockbackTimer = 0f;
    private float _knockbackDuration = 0.5f;

    private void OnEnable()
    {
        ExplodeAction.OnPlayerExploded += ExplodeAction_OnPlayerExploded;
    }
    private void OnDisable()
    {
        ExplodeAction.OnPlayerExploded -= ExplodeAction_OnPlayerExploded;
    }
    private void Awake()
    {
        Instance = this;
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerState = GetComponent<PlayerState>();
        _playerMovementInput = GetComponent<InputHandler>();
        _playerRb = GetComponent<Rigidbody2D>();
        _playerEffects = GetComponent<PlayerEffects>();
    }
    private void Update()
    {
        if (!_isKnockback)
        {
            HandlePlayerMovement();
        }
        else
        {
            _knockbackTimer -= Time.deltaTime;
            if (_knockbackTimer <= 0f && _playerMovementInput.MoveInput != Vector2.zero)
            {
                _isKnockback = false;
            }
        }
        HandlePlayerRotation();
        HandlePlayerStates();
        SignalWhenDistanceChanged();

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
            if (hit.TryGetComponent<FinishPlatform>(out FinishPlatform finish))
            {
                OnFinishPlatformReached?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                target.TakeDamage(1);
                _playerEffects.DisableTrail();
                _DealtDamage = true;
            }
        }
    }

    private void HandlePlayerMovement()
    {
        bool isInFallingState = _playerState.CurrentMovementState == PlayerMovementState.Falling;
        bool isInJumpingState = _playerState.CurrentMovementState == PlayerMovementState.Jumping;
        RaycastHit2D hit = Physics2D.CircleCast(_groundCheckPoint.position, _groundCheckRadius, Vector2.down, 0.1f, _groundLayer);
        _isGrounded = hit.collider != null && hit.normal.y > 0.5f && !isInJumpingState;

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
        float horizontalVelocity = _playerRb.linearVelocity.x;
        float epsilon = 0.01f;
        bool isSpinning = _playerAnimation.IsSpinning;

        if (!_isGrounded)
        {
            _DealtDamage = false;
            _ridingPlatformTimer = 0f;

            if (verticalVelocity > epsilon)
            {
                _playerState.SetCurrentMovementState(PlayerMovementState.Jumping);
                if (verticalVelocity == 0)
                {
                    _playerState.SetCurrentMovementState(PlayerMovementState.Idle);
                    TryDealDamageUnderPlayer();
                }
                return;
            }
            else if (verticalVelocity < -epsilon && !isSpinning)
            {
                _playerState.SetCurrentMovementState(PlayerMovementState.Falling);
                _playerMovementInput.SetJumpPressedFalse();
                return;
            }
        }
        if (_isGrounded)
        {
            if (Mathf.Abs(verticalVelocity) <= epsilon)
            {
                _ridingPlatformTimer = 0f;

                if (Mathf.Abs(horizontalVelocity) > epsilon)
                {
                    _playerState.SetCurrentMovementState(PlayerMovementState.Running);
                }
                else
                {
                    _playerState.SetCurrentMovementState(PlayerMovementState.Idle);
                    TryDealDamageUnderPlayer();
                }
            }
            else
            {
                _ridingPlatformTimer += Time.deltaTime;

                if (_ridingPlatformTimer >= _ridingPlatformDelay && Mathf.Abs(horizontalVelocity) < epsilon)
                {
                    _playerState.SetCurrentMovementState(PlayerMovementState.RidingPlatform);
                    TryDealDamageUnderPlayer();
                }
                else
                {
                    _playerState.SetCurrentMovementState(PlayerMovementState.Running);
                    TryDealDamageUnderPlayer();
                }
            }
        }
    }

    private void HandlePlayerRotation()
    {
        float playerXScale = 1f;
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
    private void ExplodeAction_OnPlayerExploded(Vector3 obj)
    {
        Vector2 direction = (Vector2)(transform.position - obj).normalized;
        float knockbackSpeed = 30f;
        _playerRb.linearVelocity = direction * knockbackSpeed;
        _isKnockback = true;
        _knockbackTimer = _knockbackDuration;
    }
    public float GetPlayerMoveSpeed()
    {
        return _moveSpeed;
    }
    public void SetPlayerSpeed(float amount)
    {
        _moveSpeed = amount;
    }
    private int CalculateDistance(float currentDistance, int charDistance)
    {
        if ((int)currentDistance > charDistance)
        {
            return (int)currentDistance;
        }

        return charDistance;
    }
    private void SignalWhenDistanceChanged()
    {
        int newDistance = CalculateDistance(transform.position.y, CharacterModel.Distance);
        if (newDistance != CharacterModel.Distance)
        {
            CharacterModel.Distance = newDistance;
            PlayerPrefsManager.SaveRecordDistance(newDistance);
            GameEventBus.ChangeDistance(newDistance);
        }
    }
}
