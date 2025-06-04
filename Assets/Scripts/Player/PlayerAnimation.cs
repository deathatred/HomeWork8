using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Player2DMovement _playerMovement;
    private PlayerState _playerState;

    private static int isJumpingHash = Animator.StringToHash("isJumping");
    private static int isFallingHash = Animator.StringToHash("isFalling");
    private static int isSpinningHash = Animator.StringToHash("isSpinning");

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
        bool isSpinning = IsSpinning;

        _animator.SetBool(isSpinningHash,isSpinning);
        _animator.SetBool(isJumpingHash, isJumping);
        _animator.SetBool(isFallingHash, isFalling);
    }

    public void SetIsSpinning(bool isSpinning)
    {
        IsSpinning = isSpinning;
    }

}
