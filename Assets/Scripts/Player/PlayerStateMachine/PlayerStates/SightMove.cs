using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputAction;

public class SightMove : StateBase
{
    private Camera m_Camera;
    private TouchScreen m_Input;

    private readonly float m_ZoomSens;
    private readonly float m_DefaultZoom;
    private readonly float m_MinZoom;
    private float m_LastFingersDistance;

    public SightMove(string stateID, SightMovementData data) : base(stateID)
    {
        m_Input = new TouchScreen();
        m_Input.SightActions.Zoom.performed += HandleZoom;

        m_ZoomSens = data.ZoomSens;
        m_DefaultZoom = data.DefaultZoom;
        m_MinZoom = data.MinZoom;

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
        context.StartCoroutine(ResetZoom());
        DisableInput();
    }

    private void HandleZoom(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count < 2) return;

        float fingerDistances = Vector2.Distance(touch[0].screenPosition, touch[1].screenPosition);

        if (touch[1].phase == UnityEngine.InputSystem.TouchPhase.Began)
            m_LastFingersDistance = fingerDistances;
        else
        {
            float deltaDistance = m_LastFingersDistance - fingerDistances;
            float zoomFactor = deltaDistance * m_ZoomSens * Time.deltaTime;
            m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView + zoomFactor, m_MinZoom, m_DefaultZoom);
            m_LastFingersDistance = fingerDistances;
        }

    }

    private IEnumerator ResetZoom()
    {
        while (Mathf.Abs(m_Camera.fieldOfView - m_DefaultZoom) > 5f)
        {
            m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, m_DefaultZoom, Time.deltaTime * m_ZoomSens);
            yield return null;
        }
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
