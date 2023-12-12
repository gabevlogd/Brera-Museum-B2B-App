using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.SingletonPattern;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GamePuzzleManager : Singleton<GamePuzzleManager>
{
    private EventManager m_EventManager;

    [Header("Puzzle Shifting")]
    [SerializeField] private bool[] m_PuzzleAmount;
    [SerializeField] private Camera m_Camera;
    [Min(0.1f)]
    [SerializeField] private float m_Velocity;

    [Header("Change Scene")]
    [SerializeField] private int m_SceneIndex; 

    private Vector3 m_NextTargetCameraPosition;

    public EventManager EventManager { get => m_EventManager; set => m_EventManager = value; }

    // Start is called before the first frame update
    void Start()
    {
        instance.EventManager = new EventManager();
        instance.EventManager.Register(Constants.SINGLE_PUZZLE_COMPLETED, UpdatePuzzleCount);
        instance.EventManager.Register(Constants.STAGE_PUZZLE_COMPLETED, BackToMuseum);
    }

    private void Update()
    {
        if (m_NextTargetCameraPosition != Vector3.zero)
        {
            m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_NextTargetCameraPosition, Time.deltaTime * m_Velocity);

            if(Vector3.Distance(m_Camera.transform.position, m_NextTargetCameraPosition) < 0.1f)
            {
                m_Camera.transform.position = m_NextTargetCameraPosition;
                m_NextTargetCameraPosition = Vector3.zero;
            }
        }
    }

    public void UpdatePuzzleCount(object[] param)
    {
        for(int i = 0; i < m_PuzzleAmount.Length; i++)
        {
            if (m_PuzzleAmount[i] == false)
            {
                m_PuzzleAmount[i] = true;
                Debug.Log("Puzzle completed");
                m_NextTargetCameraPosition = (Vector3)param[0];
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

        for (int i = 0; i < m_PuzzleAmount.Length; i++)
        {
            if (m_PuzzleAmount[i] == false)
            {
                isCompleted = false;
                break;
            }
        }

        return isCompleted;
    }

    private void BackToMuseum(object[] param)
    {
        SceneManager.LoadScene(m_SceneIndex);
    }
}
