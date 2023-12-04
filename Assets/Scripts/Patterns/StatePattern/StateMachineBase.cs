using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour 
{
    protected StateBase m_CurrentState;
    protected StateBase m_PreviousState;

    protected void RunStateMachine(StateBase entryState)
    {
        m_CurrentState = entryState;
        m_CurrentState.OnEnter(this);
    }
    public void ChangeState(StateBase state)
    {
        if (state.StateID == m_CurrentState.StateID) return;
        m_CurrentState.OnExit(this);
        m_PreviousState = m_CurrentState;
        m_CurrentState = state;
        m_CurrentState.OnEnter(this);
    }
}
