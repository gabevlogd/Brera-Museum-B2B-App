using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Movement : StateMachineBase
{

    public PreMove PreMove = new PreMove("PreMove");
    public PostMove PostMove = new PostMove("PostMove");
    public Move Move = new Move("Move");
    public Sleep Sleep = new Sleep("Sleep");

    private void Awake() => RunStateMachine(Sleep);

    private void Update()
    {
        _currentState.OnUpdate(this);
    }
}
