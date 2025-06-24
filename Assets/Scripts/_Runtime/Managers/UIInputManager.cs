using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputManager : MonoBehaviour, PlayerInputActions.IPlayerUIActions
{
    private PlayerInputActions _inputActions;
    private void Awake()
    {
        _inputActions = InputManager.Actions;
    }
    void OnEnable()
    {
        if (_inputActions != null)
        {
            _inputActions.PlayerUI.Enable();
            _inputActions.PlayerUI.SetCallbacks(this);
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
            _inputActions.PlayerUI.Disable();
            _inputActions.PlayerUI.SetCallbacks(null);
        }
        else
        {
            Debug.LogError("input Action is null cannot disable");
        }
    }
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        GameEventBus.PressEscape();
    }
}
