#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class TrackConstraint : MonoBehaviour
{

    private void Update()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}

#endif
