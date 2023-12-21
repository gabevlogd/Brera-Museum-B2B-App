using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

/// <summary>
/// This state is used for disabling all type of interaction with the 3D world by the player
/// </summary>
public class Sleep : StateBase<PlayerController>
{
    public Sleep(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
    {
    }

    
}
