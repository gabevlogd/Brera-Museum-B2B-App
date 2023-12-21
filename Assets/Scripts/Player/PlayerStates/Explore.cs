using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputAction;
using Gabevlogd.Patterns;


/// <summary>
/// This is the state of the player when standing on an anchor point, 
/// it managing the rotation and zoom of view, and looks for interactions, with the triggers around the map, performed by the player
/// </summary>
public class Explore : StateBase<PlayerController>
{
    private Camera m_Camera;
    private Transform m_PlayerTransform;
    private TouchScreen m_Input;

    private float m_YawSens;
    private float m_PitchSens;
    private float m_MaxPitch;
    private float m_MinPitch;
    private float m_YawDeceleration;

    private float m_ZoomSens;
    private float m_DefaultZoom;
    private float m_MinZoom;

    private float m_LastYawRoation;
    private float m_LastFingersDistance;
    private float m_TouchTime;

    public Explore(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
        m_Input = new TouchScreen();

    }

    public override void OnEnter(PlayerController context)
    {
        base.OnEnter(context);
        SetData(context);
        context.StartCoroutine(EnableInput(1f));
    }

    public override void OnUpdate(PlayerController context)
    {
        UpdateTouchTime();
        CheckYawRotatioReset();
        PerformYawDeceleration();
        CheckForWorldInteraction(context);
    }

    public override void OnExit(PlayerController context)
    {
        base.OnExit(context);
        m_LastYawRoation = 0f;
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
        if (Input.touchCount != 0) return;

        m_PlayerTransform.Rotate(0f, m_LastYawRoation, 0f);
        m_LastYawRoation = Mathf.Lerp(m_LastYawRoation, 0f, Time.deltaTime * m_YawDeceleration);

    }


    private void RotateYaw(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count != 1) return;
        //check if touch phase is not moved or if the movement of the finger is in the correct direction
        if (touch[0].phase != UnityEngine.InputSystem.TouchPhase.Moved || Mathf.Abs(Vector2.Dot(touch[0].delta.normalized, Vector2.right)) < 0.7f) return;

        //calculate the yaw rotation (Degrees)
        float yawRotation = Time.deltaTime * m_YawSens * Mathf.Sign(-touch[0].delta.x);
        m_PlayerTransform.Rotate(0f, yawRotation, 0f);
        m_LastYawRoation = yawRotation;
    }

    private void RotatePitch(CallbackContext context)
    {
        ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touch.Count != 1) return;
        //check if touch phase is not moved or if the movement of the finger is in the correct direction
        if (touch[0].phase != UnityEngine.InputSystem.TouchPhase.Moved || Mathf.Abs(Vector2.Dot(touch[0].delta.normalized, Vector2.up)) < 0.7f) return;
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

    private IEnumerator EnableInput(float enableDeley)
    {
        LoadInputAction();
        yield return new WaitForSeconds(enableDeley);
        m_Input.Enable();
        EnhancedTouchSupport.Enable();
    }
    private void DisableInput()
    {
        UnloadInputAction();
        m_Input.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void SetData(PlayerController context)
    {
        m_PlayerTransform = context.transform;
        m_Camera = GetCamera();

        m_ZoomSens = context.PlayerData.SightMovementData.ZoomSens;
        m_DefaultZoom = context.PlayerData.SightMovementData.DefaultZoom;
        m_MinZoom = context.PlayerData.SightMovementData.MinZoom;

        m_YawSens = context.PlayerData.SightMovementData.YawSens;
        m_PitchSens = context.PlayerData.SightMovementData.PitchSens;
        m_YawDeceleration = context.PlayerData.SightMovementData.AngularDeceleration;
        m_MaxPitch = context.PlayerData.SightMovementData.MaxPitch;
        m_MinPitch = context.PlayerData.SightMovementData.MinPitch;
    }

    private Camera GetCamera() => (m_Camera == null) ? Camera.main : m_Camera;


    private void UpdateTouchTime()
    {
        if (Input.touchCount != 1) return;
        UnityEngine.Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) m_TouchTime = 0;
        m_TouchTime += Time.deltaTime;
    }

    private GameObject GetPointedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null) return hit.collider.gameObject;
        else return null;
    }

    private void CheckForWorldInteraction(PlayerController context)
    {
        if (Input.touchCount != 1) return;
        if (m_TouchTime > 0.1f) return;

        GameObject pointedObj = GetPointedObject();
        if (pointedObj == null) return;

        FilterInteractionType(context, pointedObj);
    }

    private void FilterInteractionType(PlayerController context, GameObject pointedObj)
    {
        if (pointedObj.TryGetComponent(out MoveButton button))
        {
            SoundManager.Play("Button");
            DollyCartManager.SetDollyCart(button.Track, button.Direction);
            _stateMachine.ChangeState(context.Move);
        }
        else if (pointedObj.TryGetComponent(out PuzzleTrigger puzzleTrigger))
        {
            if (!IsPuzzleTriggerValid(context, puzzleTrigger)) return;
            UnloadInputAction();
            SceneManager.LoadScene(puzzleTrigger.TargetPuzzleSceneIndex);
        }
        else if (pointedObj.TryGetComponent(out ARTrigger arTrigger))
        {
            if (context.transform.position != arTrigger.TargetWaypoint.position) return;
            if (PlayerPrefs.GetInt(Constants.PUZZLE_FOUR) == 1) return;
            SoundManager.Play("Button");
            arTrigger.ARSystem.SetActive(true);
            _stateMachine.ChangeState(context.Sleep);
        }
    }

    private void LoadInputAction()
    {
        m_Input.SightActions.PinchZoom.performed += HandleZoom;
        m_Input.SightActions.MouseZoom.performed += HandleZoomForDevBuild;
        m_Input.SightActions.RotateSight.performed += RotateYaw;
        m_Input.SightActions.RotateSight.performed += RotatePitch;
    }
    private void UnloadInputAction()
    {
        m_Input.SightActions.PinchZoom.performed -= HandleZoom;
        m_Input.SightActions.MouseZoom.performed -= HandleZoomForDevBuild;
        m_Input.SightActions.RotateSight.performed -= RotateYaw;
        m_Input.SightActions.RotateSight.performed -= RotatePitch;
    }

    private bool IsPuzzleTriggerValid(PlayerController context, PuzzleTrigger trigger)
    {
        if (context.transform.position != trigger.TargetWaypoint.position) return false;
        switch (trigger.TargetPuzzleSceneIndex)
        {
            case 1:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_ONE) == 1) return false;
                break;
            case 2:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_TWO) == 1) return false;
                break;
            case 3:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_THREE) == 1) return false;
                break;
            default:
                return true;
        }
        return true;
    }
}
