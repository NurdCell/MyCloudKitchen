using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    // interaction evemt
    public event EventHandler OnInteractionAction;

    PlayerInputActions inputActions;
    private Vector2 moveInput;
    public Vector2 MovementInputNormalized 
    {
        get { return moveInput; }
        private set {
            moveInput = value;
        }
    }
    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => MovementInputNormalized = ctx.ReadValue<Vector2>().normalized;
        inputActions.Player.Move.canceled += ctx => MovementInputNormalized = ctx.ReadValue<Vector2>().normalized;

        inputActions.Player.Interact.performed += Interact_performed        ;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        inputActions.Player.Move?.Enable();
        inputActions.Player.Interact?.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Player.Move?.Disable();
        inputActions?.Player.Interact?.Disable();
    }
}
