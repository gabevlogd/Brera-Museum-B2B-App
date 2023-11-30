using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateBase
{
    private Transform _playerTransform;

    public Move(string stateID) : base(stateID)
    {
    }

    public override void OnEnter(StateMachineBase context)
    {
        //base.OnEnter(context);
        _playerTransform = context.transform;
    }

    public override void OnUpdate(StateMachineBase context)
    {
        //base.OnUpdate(context);

    }
}
