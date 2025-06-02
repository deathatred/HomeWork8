using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour, PlayerInputActions.IPlayerLocomotionMapActions
{
    public Vector2 MoveInput {  get; private set; }
    public bool JumpPressed { get; private set; }   

    private PlayerInputActions _inputActions;
    private PlayerState _playerState;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _playerState = GetComponent<PlayerState>();
    }
    void OnEnable()
    {
        if (_inputActions != null)
        {
            _inputActions.Enable();
            _inputActions.PlayerLocomotionMap.SetCallbacks(this);
        }
        else
        {
            Debug.LogError("input Action is null cannot enable");
        }
    }
    void OnDisable()
    {
        if (_inputActions != null)
        {
            _inputActions.Disable();
        }
        else
        {
            Debug.LogError("input Action is null cannot disable");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            JumpPressed = false;
            return;
        }
            JumpPressed = true;   
        
    }
}
