using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class Sleep : StateBase<PlayerController>
{
    public Sleep(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
    }

    //only for  build
    private float tmp = 0f;
    public override void OnUpdate(PlayerController context)
    {
        tmp += Time.deltaTime;
        if (tmp > 2f) _stateMachine.ChangeState(context.SightMove);
    }
}
