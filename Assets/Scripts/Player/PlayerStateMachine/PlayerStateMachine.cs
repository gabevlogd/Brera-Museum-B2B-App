using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    #region States:
    public Idle Idle;
    public Move Move;
    public Sleep Sleep;
    public SightMove SightMove;
    #endregion

    private void Awake()
    {
        Idle = new Idle("Idle");
        Move = new Move("Move", GetComponent<PlayerData>().MovementData);
        Sleep = new Sleep("Sleep");
        SightMove = new SightMove("SightMove", GetComponent<PlayerData>().SightMovementData);
        RunStateMachine(SightMove);
    }

    private void Update()
    {
        m_CurrentState.OnUpdate(this);
    }
}
