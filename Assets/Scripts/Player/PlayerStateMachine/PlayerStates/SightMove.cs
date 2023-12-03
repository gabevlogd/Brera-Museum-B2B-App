using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputAction;

public class SightMove : StateBase
{
    private Camera m_Camera;
    private TouchScreen m_Input;
    private float m_ZoomSens;

    public SightMove(string stateID) : base(stateID)
    {
        m_Input = new TouchScreen();
        m_Input.SightActions.Zoom.performed += HandleZoom;

        m_ZoomSens = 10000f;

    }

    public override void OnEnter(StateMachineBase context)
    {
        base.OnEnter(context);
        m_Camera = GetCamera(context);
        EnableInput();
    }

    public override void OnUpdate(StateMachineBase context)
    {
        //base.OnUpdate(context);
    }

    public override void OnExit(StateMachineBase context)
    {
        base.OnExit(context);
        DisableInput();
    }

    private void HandleZoom(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count < 2) return;

        Debug.Log($"Finger 0: {touch[0].screenPosition}");
        Debug.Log($"Finger 1: {touch[1].screenPosition}");

        m_Camera.fieldOfView = m_ZoomSens / Vector2.Distance(touch[0].screenPosition, touch[1].screenPosition);
        


    }

    private void EnableInput()
    {
        m_Input.Enable();
        EnhancedTouchSupport.Enable();
    }
    private void DisableInput()
    {
        m_Input.Disable();
        EnhancedTouchSupport.Disable();
    }

    private Camera GetCamera(StateMachineBase context) => (m_Camera == null) ? context.GetComponentInChildren<Camera>() : m_Camera;
}
