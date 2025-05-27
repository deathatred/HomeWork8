using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    void OnEnable() 
    {
        //if ()
        inputActions.Enable();
    }
    void OnDisable()
    {
        inputActions.Disable();
    }
}
