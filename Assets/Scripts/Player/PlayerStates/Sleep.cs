using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class Sleep : StateBase<PlayerController>
{
    public Sleep(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
    }

    
}
