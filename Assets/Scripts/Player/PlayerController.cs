using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;
using System;

public class PlayerController : MonoBehaviour
{
    public PlayerData PlayerData { get => m_PlayerData; }
    private PlayerData m_PlayerData;
    private static Vector3 m_LastPosition;
    private static Quaternion m_LastRotation;

    private static StateMachine<PlayerController> m_StateMachine;
    #region States:
    public Idle Idle;
    public Move Move;
    public Sleep Sleep;
    public Explore Explore;
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 1000; //to be moved to the correct place
        m_PlayerData = GetComponent<PlayerData>();
        InitializeStateMachine();
    }

    private void OnEnable()
    {
        SetTransformData();
        HUD.OnMenuOpen += PauseGame;
        HUD.OnMenuClose += ResumeGame;
        HUD.CanOpenMenu += IsPlayerExploring;
    }

    private void OnDisable()
    {
        CacheTransformData();
        HUD.OnMenuOpen -= PauseGame;
        HUD.OnMenuClose -= ResumeGame;
        HUD.CanOpenMenu -= IsPlayerExploring;
    }

    private void Update() => m_StateMachine.CurrentState.OnUpdate(this);

    private void InitializeStateMachine()
    {
        if (m_StateMachine == null)
        {
            m_StateMachine = new StateMachine<PlayerController>(this);
            InstantiatePlayerStates();
            m_StateMachine.RunStateMachine(Sleep, this);
        }
        else
        {
            InstantiatePlayerStates();
            m_StateMachine.RunStateMachine(Explore, this);
        }
    }

    private void CacheTransformData()
    {
        m_LastPosition = transform.position;
        m_LastRotation = transform.rotation;
    }

    private void SetTransformData()
    {
        if (m_LastPosition == Vector3.zero) return;
        transform.SetPositionAndRotation(m_LastPosition, m_LastRotation);
    }

    private void ResumeGame() => m_StateMachine.ChangeState(Explore);
    private void PauseGame() => m_StateMachine.ChangeState(Sleep);
    private bool IsPlayerExploring() => m_StateMachine.CurrentState == Explore ? true : false;

    private void InstantiatePlayerStates()
    {
        Idle = new Idle("Idle", m_StateMachine);
        Move = new Move("Move", m_StateMachine);
        Sleep = new Sleep("Sleep", m_StateMachine);
        Explore = new Explore("Explore", m_StateMachine);
    }
}
