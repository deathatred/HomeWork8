using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Player2DMovement _playerMovement;
    private PlayerState _playerState;

    private static int isJumpingHash = Animator.StringToHash("isJumping");
    private static int isFallingHash = Animator.StringToHash("isFalling");
    private static int isSpinningHash = Animator.StringToHash("isSpinning");
    private static int isRunningHash = Animator.StringToHash("isRunning");
    private static int isDeadHash = Animator.StringToHash("isDead");

    public bool IsSpinning { get; private set; }

    private void Awake()
    {
        _playerMovement = GetComponent<Player2DMovement>();
        _playerState = GetComponent<PlayerState>();
    }
    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        bool isJumping = _playerState.CurrentMovementState == PlayerMovementState.Jumping;
        bool isFalling = _playerState.CurrentMovementState == PlayerMovementState.Falling;
        bool isRunning = _playerState.CurrentMovementState == PlayerMovementState.Running;
        bool isSpinning = IsSpinning;
        bool isDead = GameManager.Instance.IsPlayerDead;

        _animator.SetBool(isDeadHash, isDead);
        _animator.SetBool(isSpinningHash,isSpinning);
        _animator.SetBool(isJumpingHash, isJumping);
        _animator.SetBool(isFallingHash, isFalling);
        _animator.SetBool(isRunningHash, isRunning);    
    }

    public void SetIsSpinning(bool isSpinning)
    {
        IsSpinning = isSpinning;
    }

}
