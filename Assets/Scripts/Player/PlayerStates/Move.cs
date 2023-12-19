using System;
using UnityEngine;
using Gabevlogd.Patterns;

public class Move : StateBase<PlayerController>
{
    public static event Action OnMovementEnded;
    public static event Action OnMovementStarted;

    private Transform m_PlayerTransform;
    private Camera m_Camera;
    private Action<PlayerController> m_UpdateMovement;

    private float m_AlignmentSpeed;
    private float m_AlignmentAngularSpeed;
    private float m_MovementSpeed;
    private Vector3 m_LastPosition;

    public Move(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
    }

    public override void OnEnter(PlayerController context)
    {
        base.OnEnter(context);
        OnMovementStarted?.Invoke();
        SetData(context);
    }

    public override void OnExit(PlayerController context)
    {
        base.OnExit(context);
        DollyCartManager.ResetCart();
        m_UpdateMovement = null;
        OnMovementEnded?.Invoke();
    }

    public override void OnUpdate(PlayerController context)
    {
        //base.OnUpdate(context);
        m_UpdateMovement?.Invoke(context);

    }

    private void SetPositionAndRotation(PlayerController context)
    {
        Quaternion targetRotation = DollyCartManager.GetCartRotation();
        Vector3 targetPosition = DollyCartManager.GetCartPosition();
        //checks if position and rotation of player are aligned with the cart position and rotation 
        if (Vector3.Distance(m_PlayerTransform.position, targetPosition) > 0.1f ||
            Quaternion.Dot(m_PlayerTransform.rotation, targetRotation) < 0.999f ||
            Quaternion.Dot(m_Camera.transform.rotation, targetRotation) < 0.999f)
        {
            m_PlayerTransform.position = Vector3.MoveTowards(m_PlayerTransform.position, targetPosition, Time.deltaTime * m_AlignmentSpeed);
            m_PlayerTransform.rotation = Quaternion.RotateTowards(m_PlayerTransform.rotation, targetRotation, Time.deltaTime * m_AlignmentAngularSpeed);
            m_Camera.transform.rotation = Quaternion.RotateTowards(m_Camera.transform.rotation, targetRotation, Time.deltaTime * m_AlignmentAngularSpeed);
        }
        else
        {
            DollyCartManager.StartCartMovement(m_MovementSpeed);
            m_UpdateMovement = PerformMovement;
        }
    }

    private void PerformMovement(PlayerController context)
    {
        //save player position before updateing it
        m_LastPosition = m_PlayerTransform.position;
        //update player position and rotation
        m_PlayerTransform.position = DollyCartManager.GetCartPosition();
        m_PlayerTransform.rotation = DollyCartManager.GetCartRotation();

        //checks if the motion is completed
        if (m_LastPosition == m_PlayerTransform.position)
            _stateMachine.ChangeState(_stateMachine.PreviousState);
        
    }

    private void SetData(PlayerController context)
    {
        m_AlignmentAngularSpeed = context.PlayerData.MovementData.AlignmentAngularSpeed;
        m_AlignmentSpeed = context.PlayerData.MovementData.AlignmentSpeed;
        m_MovementSpeed = context.PlayerData.MovementData.MovementSpeed;

        m_PlayerTransform = context.transform;
        m_Camera = (m_Camera == null) ? Camera.main : m_Camera;
        m_UpdateMovement = SetPositionAndRotation;
    }
}
