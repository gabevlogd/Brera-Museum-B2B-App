#if UNITY_EDITOR
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
    }

    [MenuItem("GameObject/Custom GameObject/Dolly Track")]
    public static void CreateDollyTrack()
    {
        DollyTrack trackPrefabRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDolly = Instantiate(trackPrefabRef);
        newDolly.This = newDolly.GetComponent<CinemachineSmoothPath>(); 
    }

    private void LockTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void HandleWaypoints()
    {
        if (Selection.activeGameObject != null && Selection.activeGameObject == this.gameObject) return;

        
        if (m_DollyTrack.FirstWaypointAnchorTrack != null && m_DollyTrack.FirstWaypointAnchorIndex < m_DollyTrack.FirstWaypointAnchorTrack.m_Waypoints.Length)
        {
            Vector3 targetCostraintPos = m_DollyTrack.FirstWaypointAnchorTrack.m_Waypoints[m_DollyTrack.FirstWaypointAnchorIndex].position;
            m_DollyTrack.This.m_Waypoints[0].position.Set(targetCostraintPos.x, targetCostraintPos.y, targetCostraintPos.z);
        }
        if (m_DollyTrack.LastWaypointAnchorTrack != null && m_DollyTrack.LastWaypointAnchorIndex < m_DollyTrack.LastWaypointAnchorTrack.m_Waypoints.Length)
        {
            Vector3 targetCostraintPos = m_DollyTrack.LastWaypointAnchorTrack.m_Waypoints[m_DollyTrack.LastWaypointAnchorIndex].position;
            m_DollyTrack.This.m_Waypoints[^1].position.Set(targetCostraintPos.x, targetCostraintPos.y, targetCostraintPos.z);
        }
        
    }
}

#endif