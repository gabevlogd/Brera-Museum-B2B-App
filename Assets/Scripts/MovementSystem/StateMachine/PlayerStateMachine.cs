using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    #region States:
    public Idle Idle = new Idle("Idle");
    public Move Move = new Move("Move");
    public Sleep Sleep = new Sleep("Sleep");
    #endregion

    private void Awake() => RunStateMachine(Sleep);

    private void Update()
    {
        _currentState.OnUpdate(this);
    }
}
