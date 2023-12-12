using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugger : MonoBehaviour
{
    private PlayerController m_PlayerSM;
    public bool IdleState;
    public bool MoveState;
    public bool SightMoveState;

    private void Awake()
    {
        m_PlayerSM = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (IdleState)
        {
            m_PlayerSM.m_StateMachine.ChangeState(m_PlayerSM.Idle);
            IdleState = false;
        }
        else if (MoveState)
        {
            m_PlayerSM.m_StateMachine.ChangeState(m_PlayerSM.Move);
            MoveState = false;
        }
        else if (SightMoveState)
        {
            m_PlayerSM.m_StateMachine.ChangeState(m_PlayerSM.SightMove);
            SightMoveState = false;
        }
    }
}
