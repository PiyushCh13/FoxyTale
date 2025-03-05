using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 movementVector { get; private set; }
    public event Action OnJumpPressed,OnJumpReleased, OnDoubleJump;
    public event Action<Vector2> OnMovementVector;
    public event Action OnPress;

    private void Update()
    {
        OnMovement();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpPressed?.Invoke();
        }

        if (context.canceled)
        {
            OnJumpReleased?.Invoke();
        }
    }

    public void Pressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPress?.Invoke();
        }
    }

    public void DoubleJump(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            OnDoubleJump?.Invoke();
        }
    }
    public void GetMovementVector(InputAction.CallbackContext context)
    {
        movementVector = new Vector2(context.ReadValue<Vector2>().x, 0);
    }

    public void OnMovement()
    {
        OnMovementVector?.Invoke(movementVector);
    }
}
