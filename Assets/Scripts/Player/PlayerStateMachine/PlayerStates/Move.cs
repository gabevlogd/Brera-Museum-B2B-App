using System;
using UnityEngine;

public class Move : StateBase
{
    private Transform m_PlayerTransform;
    private Action<StateMachineBase> m_UpdateMovement;

    private readonly float m_AlignmentSpeed;
    private readonly float m_AlignmentAngularSpeed;
    private readonly float m_MovementSpeed;
    private Vector3 m_LastPosition;

    public Move(string stateID, MovementData movementData) : base(stateID)
    {
        m_AlignmentAngularSpeed = movementData.AlignmentAngularSpeed;
        m_AlignmentSpeed = movementData.AlignmentSpeed;
        m_MovementSpeed = movementData.MovementSpeed;
    }

    public override void OnEnter(StateMachineBase context)
    {
        base.OnEnter(context);
        m_PlayerTransform = context.transform;
        m_UpdateMovement = SetPositionAndRotation;
    }

    public override void OnExit(StateMachineBase context)
    {
        base.OnExit(context);
        DollyCartManager.ResetCart();
        m_UpdateMovement = null;
    }

    public override void OnUpdate(StateMachineBase context)
    {
        //base.OnUpdate(context);
        m_UpdateMovement?.Invoke(context);

    }

    private void SetPositionAndRotation(StateMachineBase context)
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

    private void PerformMovement(StateMachineBase context)
    {
        //cast the context
        PlayerStateMachine playerSM = context as PlayerStateMachine;
        //save player position before updateing it
        m_LastPosition = m_PlayerTransform.position;
        //update player position and rotation
        m_PlayerTransform.position = DollyCartManager.GetCartPosition();
        m_PlayerTransform.rotation = DollyCartManager.GetCartRotation();

        //checks if the motion is completed
        if (m_LastPosition == m_PlayerTransform.position)
            playerSM.ChangeState(playerSM.Idle);
        
    }
}
