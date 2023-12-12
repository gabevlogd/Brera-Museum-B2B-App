using System;
using UnityEngine;
using Gabevlogd.Patterns;

public class Move : StateBase<PlayerController>
{
    public static event Action OnMovementEnded;
    public static event Action OnMovementStarted;

    private Transform m_PlayerTransform;
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
        //checks if position and rotation of player are aligned with the cart position and rotation 
        if (Vector3.Distance(m_PlayerTransform.position, DollyCartManager.GetCartPosition()) > 0.1f ||
            Mathf.Abs(Quaternion.Dot(m_PlayerTransform.rotation, DollyCartManager.GetCartRotation()) - 1f) > 0.0001f)
        {
            m_PlayerTransform.position = Vector3.MoveTowards(m_PlayerTransform.position, DollyCartManager.GetCartPosition(), Time.deltaTime * m_AlignmentSpeed);
            m_PlayerTransform.rotation = Quaternion.RotateTowards(m_PlayerTransform.rotation, DollyCartManager.GetCartRotation(), Time.deltaTime * m_AlignmentAngularSpeed);
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
        m_UpdateMovement = SetPositionAndRotation;
    }
}
