#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

[ExecuteInEditMode]
public class TrackHandler : MonoBehaviour
{
    private DollyTrack m_DollyTrack;

    private void Awake() => m_DollyTrack = GetComponent<DollyTrack>();

    private void Update()
    {
        LockTransform();
        HandleWaypoints();
        HandleTrackButtons();
    }

    private void LockTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void HandleWaypoints()
    {
        if (m_DollyTrack.FirstAnchorPoint != null)
            m_DollyTrack.This.m_Waypoints[0].position = m_DollyTrack.FirstAnchorPoint.transform.position;
        if (m_DollyTrack.SecondAnchorPoint != null)
            m_DollyTrack.This.m_Waypoints[^1].position = m_DollyTrack.SecondAnchorPoint.transform.position;
    }

    private void HandleTrackButtons()
    {
        Vector3 firstTargetPosition = m_DollyTrack.This.m_Waypoints[0].position;
        Vector3 secondTargetPosition = m_DollyTrack.This.m_Waypoints[^1].position;
        m_DollyTrack.FirstButton.transform.position = new Vector3(firstTargetPosition.x, m_DollyTrack.FirstButton.transform.position.y, firstTargetPosition.z);
        m_DollyTrack.SecondButton.transform.position = new Vector3(secondTargetPosition.x, m_DollyTrack.SecondButton.transform.position.y, secondTargetPosition.z);
    }
}

#endif