using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    public event Action OnPress;
    public event Action OnEnterLevel;
    [HideInInspector] public Vector2 OnLevelPlayerMove { get; private set; }
    public event Action<Vector2> OnLevelPlayerMoveAction;

    public void Pressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPress?.Invoke();
        }
    }

    public void PressEnter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnEnterLevel?.Invoke();
        }
    }

    public void GetOnLevelPlayerMove(InputAction.CallbackContext context)
    {
        OnLevelPlayerMove = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }
}
