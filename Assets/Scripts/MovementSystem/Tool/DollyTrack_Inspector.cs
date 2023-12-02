#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Cinemachine;

[CustomEditor(typeof(DollyTrack))]
public class DollyTrack_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawNewTrackButton();
        
    }

    private void DrawNewTrackButton()
    {
        if (GUILayout.Button("New Track"))
            PerformNewTrackButton();
    }

    private void PerformNewTrackButton()
    {
        DollyTrack currentDolly = (DollyTrack)target;
        DollyTrack newDollyAssetRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDollyInstanceRef = Instantiate(newDollyAssetRef);

        newDollyInstanceRef.This = newDollyInstanceRef.GetComponent<CinemachineSmoothPath>();
        newDollyInstanceRef.This.m_Waypoints[0].position = currentDolly.This.m_Waypoints[^1].position;
        newDollyInstanceRef.This.m_Waypoints[1].position = newDollyInstanceRef.This.m_Waypoints[0].position + new Vector3(0f, 0f, 10f);

        newDollyInstanceRef.FirstWaypointAnchorTrack = currentDolly.GetComponent<CinemachineSmoothPath>();
        newDollyInstanceRef.FirstWaypointAnchorIndex = currentDolly.GetComponent<CinemachineSmoothPath>().m_Waypoints.Length - 1;

        currentDolly.LastWaypointAnchorTrack = newDollyInstanceRef.GetComponent<CinemachineSmoothPath>();
        currentDolly.LastWaypointAnchorIndex = 0;
    }
}

#endif
