using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputAction;

public class SightMove : StateBase
{
    private Camera m_Camera;
    private Transform m_PlayerTransform;
    private TouchScreen m_Input;

    private readonly float m_YawSens;
    private readonly float m_PitchSens;
    private readonly float m_MaxPitch;
    private readonly float m_MinPitch;
    private readonly float m_YawDeceleration;

    private readonly float m_ZoomSens;
    private readonly float m_DefaultZoom;
    private readonly float m_MinZoom;

    private float m_LastYawRoation;
    private float m_LastFingersDistance;

    public SightMove(string stateID, SightMovementData data) : base(stateID)
    {
        m_Input = new TouchScreen();
        m_Input.SightActions.PinchZoom.performed += HandleZoom;
        m_Input.SightActions.MouseZoom.performed += HandleZoomForDevBuild;
        m_Input.SightActions.RotateSight.performed += RotateYaw;
        m_Input.SightActions.RotateSight.performed += RotatePitch;

        m_ZoomSens = data.ZoomSens;
        m_DefaultZoom = data.DefaultZoom;
        m_MinZoom = data.MinZoom;

        m_YawSens = data.YawSens;
        m_PitchSens = data.PitchSens;
        m_YawDeceleration = data.AngularDeceleration;
        m_MaxPitch = data.MaxPitch;
        m_MinPitch = data.MinPitch;

    }

    public override void OnEnter(StateMachineBase context)
    {
        base.OnEnter(context);
        m_PlayerTransform = context.transform;
        m_Camera = GetCamera(context);
        EnableInput();
    }

    public override void OnUpdate(StateMachineBase context)
    {
        //base.OnUpdate(context);
        CheckYawRotatioReset();
        PerformYawDeceleration();
    }

    public override void OnExit(StateMachineBase context)
    {
        base.OnExit(context);
        context.StartCoroutine(ResetZoom());
        DisableInput();
    }

    #region Rotations:

    private void CheckYawRotatioReset()
    {
        if (Input.touchCount != 0)
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                m_LastYawRoation = 0;
    }

    private void PerformYawDeceleration()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count != 0) return;

        m_PlayerTransform.Rotate(0f, m_LastYawRoation, 0f);
        m_LastYawRoation = Mathf.Lerp(m_LastYawRoation, 0f, Time.deltaTime * m_YawDeceleration);
        
    }


    private void RotateYaw(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count != 1) return;
        //check if touch phase is not moved or if the movement of the finger is in the deadzone area (0 pixel <= deadZone < 5 pixel)
        if (touch[0].phase != UnityEngine.InputSystem.TouchPhase.Moved || Mathf.Abs(touch[0].delta.x) < 5f) return;
        
        //calculate the yaw rotation (Degrees)
        float yawRotation = Time.deltaTime * m_YawSens * Mathf.Sign(-touch[0].delta.x);
        m_PlayerTransform.Rotate(0f, yawRotation, 0f);
        m_LastYawRoation = yawRotation;
    }

    private void RotatePitch(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count != 1) return;
        //check if touch phase is not moved or if the movement of the finger is in the deadzone area (0 pixel <= deadZone < 5 pixel)
        if (touch[0].phase != UnityEngine.InputSystem.TouchPhase.Moved || Mathf.Abs(touch[0].delta.y) < 5f) return;
        //calculate the angle between forward of player and forward of camera
        float angle = Vector3.SignedAngle(m_Camera.transform.forward, m_PlayerTransform.forward, m_PlayerTransform.right);
        //calculate the pitch rotation (Degrees)
        float pitchRotation = Time.deltaTime * m_PitchSens * Mathf.Sign(touch[0].delta.y);
        //conditions for clamping pitch rotation between a max and a min values
        if (angle > m_MaxPitch)
        {
            if (touch[0].delta.y > 0) m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
        }
        else if (angle < m_MinPitch)
        {
            if (touch[0].delta.y < 0) m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
        }
        else m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
    }

    #endregion

    #region Zoom:

    private void HandleZoom(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count != 2) return;

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

    private void HandleZoomForDevBuild(CallbackContext context)
    {
        float mouseZoomSens = 500f;
        if (context.ReadValue<Vector2>().y < 0)
            m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView + mouseZoomSens * Time.deltaTime, m_MinZoom, m_DefaultZoom);
        else if (context.ReadValue<Vector2>().y > 0)
            m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView - mouseZoomSens * Time.deltaTime, m_MinZoom, m_DefaultZoom);

    }

    private IEnumerator ResetZoom()
    {
        while (Mathf.Abs(m_Camera.fieldOfView - m_DefaultZoom) > 5f)
        {
            m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, m_DefaultZoom, Time.deltaTime * m_ZoomSens);
            yield return null;
        }
    }

    #endregion

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
