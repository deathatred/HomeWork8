using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerMovementState CurrentMovementState { get; private set; }

    [SerializeField] private PlayerMovementState currMovStateDebugField;

    private void Update()
    {
        UpdateDebugField();
    }
    public void SetCurrentMovementState(PlayerMovementState movementState)
    {
        CurrentMovementState = movementState;
    }
    private void UpdateDebugField()
    {
        currMovStateDebugField = CurrentMovementState;
    }
}


public enum PlayerMovementState
{
    Idle,
    Jumping,
    Falling,
    RidingPlatform,
    Running,
    Dashing
}
