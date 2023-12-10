using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.SingletonPattern;
using Cinemachine;

public class GamePuzzleManager : Singleton<GamePuzzleManager>
{
    private EventManager m_EventManager;

    [SerializeField] private bool[] m_PuzzleAmount;
    [SerializeField] private PlayerStateMachine m_PlayerSM;

    public EventManager EventManager { get => m_EventManager; set => m_EventManager = value; }

    private void Awake()
    {
        instance.EventManager = new EventManager();
    }
    // Start is called before the first frame update
    void Start()
    {
        instance.EventManager.Register(Constants.SINGLE_PUZZLE_COMPLETED, UpdatePuzzleCount);
    }

    public void UpdatePuzzleCount(object[] param)
    {
        int i = 0;
        while (i < m_PuzzleAmount.Length)
        {
            if (m_PuzzleAmount[i] == false)
            {
                m_PuzzleAmount[i] = true;
                Debug.Log("Puzzle completed");
                DollyCartManager.SetDollyCart((CinemachineSmoothPath)param[0], (TrackDirection)param[1]);
                m_PlayerSM.ChangeState(m_PlayerSM.Move);
                break;
            }
        }

        if (CheckLevelCompleted())
        {
            instance.EventManager.TriggerEvent(Constants.STAGE_PUZZLE_COMPLETED);
        }
    }

    private bool CheckLevelCompleted()
    {
        bool isCompleted = true;

        int i = 0;
        while (i < m_PuzzleAmount.Length)
        {
            if (m_PuzzleAmount[i] == false)
            {
                isCompleted = false;
                break;
            }
        }

        return isCompleted;
    }
}
