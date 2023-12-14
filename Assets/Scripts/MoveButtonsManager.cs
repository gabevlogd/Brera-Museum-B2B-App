using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonsManager : MonoBehaviour
{
    public static AnchorPoint TargetAnchorPoint; //only for build

    private AnchorPoint[] m_AnchorPoints;
    private AnchorPoint m_CurrentAnchorPoint;
    private Transform m_PlayerTransform;

    private void Awake()
    {
        m_AnchorPoints = FindObjectsByType<AnchorPoint>(FindObjectsSortMode.None);
        m_PlayerTransform = FindObjectOfType<PlayerData>().transform;
        if (TargetAnchorPoint == null) TargetAnchorPoint = GetCurrentAnchorPoint(); // only for build
        InitializeManager(TargetAnchorPoint);
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
        if (m_CurrentAnchorPoint == null) return;
        foreach (MoveButton btn in m_CurrentAnchorPoint.ButtonsList)
            btn.gameObject.SetActive(false);
    }

    private void UpdateActivesButtons()
    {
        m_CurrentAnchorPoint = GetCurrentAnchorPoint();
        if (m_CurrentAnchorPoint == null) return;

        foreach (MoveButton btn in m_CurrentAnchorPoint.ButtonsList)
            btn.gameObject.SetActive(true);
        
    }

    private void InitializeManager(AnchorPoint startingAnchorPoint)
    {
        m_CurrentAnchorPoint = startingAnchorPoint;
        if (m_CurrentAnchorPoint == null) return;

        m_PlayerTransform.position = m_CurrentAnchorPoint.transform.position;
        m_PlayerTransform.rotation = m_CurrentAnchorPoint.transform.rotation;
        for (int i = 0; i < m_AnchorPoints.Length; i++)
        {
            for (int j = 0; j < m_AnchorPoints[i].ButtonsList.Count; j++)
            {
                if (m_AnchorPoints[i] != m_CurrentAnchorPoint && m_AnchorPoints[i].ButtonsList != null)
                    m_AnchorPoints[i].ButtonsList[j]?.gameObject.SetActive(false);
            }
        }
    }


    private AnchorPoint GetCurrentAnchorPoint()
    {
        if (m_AnchorPoints == null) return null;

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
