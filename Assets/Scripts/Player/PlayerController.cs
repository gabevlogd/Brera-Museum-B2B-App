using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class PlayerController : MonoBehaviour
{
    public PlayerData PlayerData { get => m_PlayerData; }
    private PlayerData m_PlayerData;
    private static Vector3 m_LastPosition;
    private static Quaternion m_LastRotation;

    public StateMachine<PlayerController> m_StateMachine;
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

    private void OnEnable() => SetTransformData();
    private void OnDisable() => CacheTransformData();

    private void Update() => m_StateMachine.CurrentState.OnUpdate(this);

    private void InitializeStateMachine()
    {
        m_StateMachine = new StateMachine<PlayerController>(this);
        Idle = new Idle("Idle", m_StateMachine);
        Move = new Move("Move", m_StateMachine);
        Sleep = new Sleep("Sleep", m_StateMachine);
        Explore = new Explore("Explore", m_StateMachine);
        m_StateMachine.RunStateMachine(Explore);
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
}
