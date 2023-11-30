#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Cinemachine;

[ExecuteInEditMode]
public class TrackHandler : MonoBehaviour
{
    private DollyTrack _dollyTrack;

    private void Awake() => _dollyTrack = GetComponent<DollyTrack>();

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

        
        if (_dollyTrack.FirstWaypointAnchorTrack != null && _dollyTrack.FirstWaypointAnchorIndex < _dollyTrack.FirstWaypointAnchorTrack.m_Waypoints.Length)
        {
            Vector3 targetCostraintPos = _dollyTrack.FirstWaypointAnchorTrack.m_Waypoints[_dollyTrack.FirstWaypointAnchorIndex].position;
            _dollyTrack.This.m_Waypoints[0].position.Set(targetCostraintPos.x, targetCostraintPos.y, targetCostraintPos.z);
        }
        if (_dollyTrack.LastWaypointAnchorTrack != null && _dollyTrack.LastWaypointAnchorIndex < _dollyTrack.LastWaypointAnchorTrack.m_Waypoints.Length)
        {
            Vector3 targetCostraintPos = _dollyTrack.LastWaypointAnchorTrack.m_Waypoints[_dollyTrack.LastWaypointAnchorIndex].position;
            _dollyTrack.This.m_Waypoints[^1].position.Set(targetCostraintPos.x, targetCostraintPos.y, targetCostraintPos.z);
        }
        
    }
}

#endif