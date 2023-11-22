using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour 
{
    protected StateBase _currentState;
    protected StateBase _previousState;

    protected void RunStateMachine(StateBase entryState)
    {
        _currentState = entryState;
        _currentState.OnEnter(this);
    }
    protected void ChangeState(StateBase state)
    {
        _currentState.OnExit(this);
        _previousState = _currentState;
        _currentState = state;
        _currentState.OnEnter(this);
    }
}
