using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonsManager : MonoBehaviour
{
    private AnchorPoint[] m_AnchorPoints;
    private AnchorPoint m_CurrentAnchorPoint;
    private Transform m_PlayerTransform;

    private void Awake()
    {
        m_AnchorPoints = FindObjectsByType<AnchorPoint>(FindObjectsSortMode.None);
        m_PlayerTransform = FindObjectOfType<PlayerData>().transform;
        InitializeManager();
    }

    private void OnEnable()
    {
        Move.OnMovementEnded += UpdateActivesButtons;
        Move.OnMovementStarted += DisableButtons;
    }
    private void OnDisable()
    {
        Move.OnMovementEnded -= UpdateActivesButtons;
        Move.OnMovementStarted -= DisableButtons;
    }

    private void DisableButtons()
    {
        foreach (MoveButton btn in m_CurrentAnchorPoint.ButtonsList)
            btn.gameObject.SetActive(false);
    }

    private void UpdateActivesButtons()
    {
        m_CurrentAnchorPoint = GetCurrentAnchorPoint();
        foreach (MoveButton btn in m_CurrentAnchorPoint.ButtonsList)
        {
            btn.gameObject.SetActive(true);
        }
    }

    private void InitializeManager()
    {
        m_CurrentAnchorPoint = GetCurrentAnchorPoint();
        m_PlayerTransform.position = m_CurrentAnchorPoint.transform.position;
        for (int i = 0; i < m_AnchorPoints.Length; i++)
        {
            for (int j = 0; j < m_AnchorPoints[i].ButtonsList.Count; j++)
            {
                //m_AnchorPoints[i].ButtonsList?.TrimExcess();
                //m_AnchorPoints[i].ButtonsList[j]?.gameObject.SetActive(false);
                if (m_AnchorPoints[i] != m_CurrentAnchorPoint && m_AnchorPoints[i].ButtonsList != null)
                    m_AnchorPoints[i].ButtonsList[j]?.gameObject.SetActive(false);

            }
        }

        //UpdateActivesButtons();
    }


    private AnchorPoint GetCurrentAnchorPoint()
    {
        AnchorPoint nearestAnchorPoint = m_AnchorPoints[0];
        float nearestDistance = Vector3.Distance(m_PlayerTransform.position, nearestAnchorPoint.transform.position);
        for(int i = 1; i < m_AnchorPoints.Length; i++)
        {
            float currentDistance = Vector3.Distance(m_PlayerTransform.position, m_AnchorPoints[i].transform.position);
            if (nearestDistance > currentDistance)
            {
                nearestDistance = currentDistance;
                nearestAnchorPoint = m_AnchorPoints[i];
            }
        }

        return nearestAnchorPoint;
    }

}
