using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions Actions { get; private set; }

    public static event Action<InputAction, int> OnBindingChanged;

    private void Awake()
    {
        if (Actions == null)
        {
            Actions = new PlayerInputActions();
            Actions.Enable();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void RaiseBindingChanged(InputAction action, int bindingIndex)
    {
        OnBindingChanged?.Invoke(action, bindingIndex);
    }
}
