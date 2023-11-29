#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class TrackConstraint : MonoBehaviour
{
    private DollyTrack _dollyTrack;

    private void Update()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        if (_dollyTrack == null)
            _dollyTrack = GetComponent<DollyTrack>();
        else if (_dollyTrack.ID > 1)
        {
            Vector3 targetCostraintPos = _dollyTrack.AnchoredTrack.m_Waypoints[^1].position;
            _dollyTrack.This.m_Waypoints[0].position.Set(targetCostraintPos.x, targetCostraintPos.y, targetCostraintPos.z);
        }

    }

    private void OnDestroy()
    {
        if (transform.parent.TryGetComponent(out DollyTracksManager manager))
        {
            for (int i = _dollyTrack.ID; i < manager.TracksList.Count; i++)
            {
                manager.TracksList[i].ID--;
            }

            if (manager.TracksList.Contains(GetComponent<DollyTrack>())) 
                manager.TracksList.RemoveAt(_dollyTrack.ID - 1);

        }
    }
}

#endif
