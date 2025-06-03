using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, PlayerInputActions.IPlayerLocomotionMapActions,
     PlayerInputActions.IPlayerActionMapActions
{
    public bool FirstSpellPressed { get; private set; }
    public bool SecondSpellPressed { get; private set; }
    public Vector2 MoveInput { get; private set; }
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
            _inputActions.PlayerActionMap.SetCallbacks(this);
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
        if (context.started)
        {
            JumpPressed = true;
        }
    }
    public void SetFirstSpellPressedFalse()
    {
        FirstSpellPressed = false;
    }
    public void SetSecondSpellPressedFalse()
    {
        SecondSpellPressed = false;
    }
    public void SetJumpPressedFalse()
    {
        JumpPressed = false;
    }

    public void OnFirstSkill(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FirstSpellPressed = true;
        }
    }

    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SecondSpellPressed = true;
        }
    }
}
