using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    #region States:
    public Idle Idle;
    public Move Move;
    public Sleep Sleep;
    #endregion

    private void Awake()
    {
        Idle = new Idle("Idle");
        Move = new Move("Move", GetComponent<PlayerData>().MovementData);
        Sleep = new Sleep("Sleep");
        RunStateMachine(Idle);
    }

    private void Update()
    {
        m_CurrentState.OnUpdate(this);
    }
}
