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
        newDollyInstanceRef.FirstAnchorPoint = currentDolly.SecondAnchorPoint;
        newDollyInstanceRef.This.m_Waypoints[1].position = newDollyInstanceRef.FirstAnchorPoint.transform.position + new Vector3(0f, 0f, 10f);
    }

    [MenuItem("GameObject/Custom GameObject/Dolly Track")]
    public static void CreateDollyTrack()
    {
        DollyTrack trackPrefabRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDolly = Instantiate(trackPrefabRef);
        newDolly.This = newDolly.GetComponent<CinemachineSmoothPath>();

        newDolly.FirstButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[0].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        newDolly.SecondButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[^1].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        newDolly.FirstButton.Track = newDolly.This;
        newDolly.FirstButton.Direction = TrackDirection.Backward;
        newDolly.SecondButton.Track = newDolly.This;
        newDolly.SecondButton.Direction = TrackDirection.Forward;
    }
}

#endif
