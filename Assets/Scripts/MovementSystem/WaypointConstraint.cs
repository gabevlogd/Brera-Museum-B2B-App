#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Cinemachine.CinemachineSmoothPath;

[ExecuteInEditMode]
public class WaypointConstraint : MonoBehaviour
{
    private CinemachineSmoothPath _previousDollyTrack;
    private CinemachineSmoothPath _dollyTrack;
    private DollyTracksManager _dollyTracksManager;
    private Waypoint[] _waypoints;


    private void Update()
    {
        if (_dollyTrack == null)
        {
            _dollyTrack = GetComponent<CinemachineSmoothPath>();
            _dollyTracksManager = FindAnyObjectByType<DollyTracksManager>();
            _waypoints = _dollyTrack.m_Waypoints;
            //_dollyTracksManager.TracksList.Find(_dollyTrack);
        }

        //_waypoints[0].position = 
    }
}

#endif
