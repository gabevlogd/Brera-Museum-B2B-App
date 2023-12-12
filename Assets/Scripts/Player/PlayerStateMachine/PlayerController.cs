using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class PlayerController : MonoBehaviour
{
    public PlayerData PlayerData { get => m_PlayerData; }
    private PlayerData m_PlayerData;

    public StateMachine<PlayerController> m_StateMachine;
    #region States:
    public Idle Idle;
    public Move Move;
    public Sleep Sleep;
    public SightMove SightMove;
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 1000;
        m_PlayerData = GetComponent<PlayerData>();
        InitializeStateMachine();
    }

    private void Update()
    {
        m_StateMachine.CurrentState.OnUpdate(this);
    }

    private void InitializeStateMachine()
    {
        m_StateMachine = new StateMachine<PlayerController>(this);
        Idle = new Idle("Idle", m_StateMachine);
        Move = new Move("Move", m_StateMachine);
        Sleep = new Sleep("Sleep", m_StateMachine);
        SightMove = new SightMove("SightMove", m_StateMachine);
        m_StateMachine.RunStateMachine(SightMove);
    }
}
