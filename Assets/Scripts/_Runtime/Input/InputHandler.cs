using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, PlayerInputActions.IPlayerLocomotionMapActions,
     PlayerInputActions.IPlayerActionMapActions
{
    public static InputHandler Instance { get; private set; }
    public bool FirstSpellPressed { get; private set; }
    public bool SecondSpellPressed { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }

    private PlayerInputActions _inputActions;
    private PlayerState _playerState;

    public bool[] SpellPressedArray;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("there are 2 instances of Input Handler");
        }

        //_inputActions = new PlayerInputActions();
        _inputActions = InputManager.Actions;
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
    public bool IsSpellPressed(int index)
    {
        if (index == 0)
        {
            return FirstSpellPressed;
        }
        if (index == 1)
        {
            return SecondSpellPressed;
        }
        else
        {
            Debug.LogError($"{index} Spell not found");
            return false;
        }


    }
    public void SetSpellPressedFalse(int index)
    {
        if (index == 0)
        {
            FirstSpellPressed = false;
            return;
        }
        if (index == 1)
        {
            SecondSpellPressed = false;
            return;
        }
        else
        {

            Debug.LogError($"{index} Spell not found");
        }
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
    public void SetJumpPressedFalse()
    {
        JumpPressed = false;
    }
    public PlayerInputActions GetPlayerInputActions()
    {
        return _inputActions;
    }
}
