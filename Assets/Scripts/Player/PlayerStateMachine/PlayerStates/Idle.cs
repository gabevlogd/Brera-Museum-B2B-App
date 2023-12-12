using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class Idle : StateBase<PlayerController>
{
    public Idle(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
    }

    public override void OnUpdate(PlayerController context)
    {
        //base.OnUpdate(context);
    }
}
