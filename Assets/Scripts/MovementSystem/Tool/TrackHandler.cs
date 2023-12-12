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
        //if (m_DollyTrack.FirstAnchorPoint != null)
        //{
        //    if (m_DollyTrack.FirstAnchorPoint.ButtonsList == null)
        //        m_DollyTrack.FirstAnchorPoint.ButtonsList = new List<MoveButton>() { m_DollyTrack.SecondButton };
        //    else if (!m_DollyTrack.FirstAnchorPoint.ButtonsList.Contains(m_DollyTrack.SecondButton))
        //        m_DollyTrack.FirstAnchorPoint.ButtonsList.Add(m_DollyTrack.SecondButton);
        //}
            
        //if (m_DollyTrack.SecondAnchorPoint != null)
        //{
        //    if (m_DollyTrack.SecondAnchorPoint.ButtonsList == null)
        //        m_DollyTrack.SecondAnchorPoint.ButtonsList = new List<MoveButton>() { m_DollyTrack.FirstButton };
        //    else if (!m_DollyTrack.SecondAnchorPoint.ButtonsList.Contains(m_DollyTrack.FirstButton))
        //        m_DollyTrack.SecondAnchorPoint.ButtonsList.Add(m_DollyTrack.FirstButton);
        //}
    }
}

#endif