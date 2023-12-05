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

    private float m_LastFingerPosX;
    private float m_LastFingerPosY;
    private float m_LastYawRoation;
    private float m_LastFingersDistance;

    private bool m_CanDecelerate;

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
        PerformYawDeceleration();
    }

    public override void OnExit(StateMachineBase context)
    {
        base.OnExit(context);
        context.StartCoroutine(ResetZoom());
        DisableInput();
    }

    #region Rotations:

    private void PerformYawDeceleration()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count != 0 || !m_CanDecelerate) return;

        m_PlayerTransform.Rotate(0f, m_LastYawRoation, 0f);
        m_LastYawRoation = Mathf.Lerp(m_LastYawRoation, 0f, Time.deltaTime * m_YawDeceleration);

        if (Mathf.Abs(m_LastYawRoation) <= 0) m_CanDecelerate = false;
        
    }


    private void RotateYaw(CallbackContext context)
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count != 1) return;

        //read finger 0 position
        Vector2 fingerPos = context.ReadValue<Vector2>();
        //calculate delta position between last finger position X and current finger position X
        float deltaPos = (m_LastFingerPosX -fingerPos.x);
        //check if the movement of the finger is in the deadzone area (0 pixel <= deadZone < 20 pixel)
        if (Mathf.Abs(deltaPos) < 20f)
        {
            m_CanDecelerate = false;
            return;
        }
        //calculate the yaw rotation (Degrees)
        float yawRotation = Time.deltaTime * m_YawSens * Mathf.Sign(deltaPos);
        m_PlayerTransform.Rotate(0f, yawRotation, 0f);
        //cache last finger position x and last yaw rotation value
        m_LastFingerPosX = fingerPos.x;
        m_LastYawRoation = yawRotation;
        m_CanDecelerate = true;
    }

    private void RotatePitch(CallbackContext context)
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count != 1) return;

        //read finger 0 position
        Vector2 fingerPos = context.ReadValue<Vector2>();
        //calculate delta position between last finger position Y and current finger position Y
        float deltaPos = fingerPos.y - m_LastFingerPosY;
        //check if the movement of the finger is in the deadzone area (0 pixel <= deadZone < 20 pixel)
        if (Mathf.Abs(deltaPos) < 20f) return;
        //calculate the angle between forward of player and forward of camera
        float angle = Vector3.SignedAngle(m_Camera.transform.forward, m_PlayerTransform.forward, m_PlayerTransform.right);
        //calculate the pitch rotation (Degrees)
        float pitchRotation = Time.deltaTime * m_PitchSens * Mathf.Sign(deltaPos);
        //conditions for clamping pitch rotation between a max and a min values
        if (angle > m_MaxPitch)
        {
            if (deltaPos > 0) m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
        }
        else if (angle < m_MinPitch)
        {
            if (deltaPos < 0) m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
        }
        else m_Camera.transform.Rotate(pitchRotation, 0f, 0f);
        //cache last finger position y
        m_LastFingerPosY = fingerPos.y;
        //needed to avoid strange artifact
        m_CanDecelerate = false;
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
        float mouseZoomSens = 1000f;
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
