using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerMovementState CurrentMovementState { get; private set; }

    [SerializeField] private PlayerMovementState currMovStateDebugField;

    private void Awake()
    {

    }
    private void Update()
    {
        currMovStateDebugField = CurrentMovementState;
    }
    public void SetCurrentMovementState(PlayerMovementState movementState)
    {
        CurrentMovementState = movementState;
    }
}

public enum PlayerMovementState
{
    Idle,
    Jumping,
    Falling,
}
