using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    public List<MoveButton> ButtonsList;

#if UNITY_EDITOR
    [MenuItem("GameObject/Custom GameObject/Anchor Point")]
    public static void CreateAnchorPoint()
    {
        AnchorPoint anchorPointPrefabRef = Resources.Load<AnchorPoint>("Anchor Point");
        AnchorPoint newAnchorPoint = Instantiate(anchorPointPrefabRef);
        newAnchorPoint.name = "Anchor Point";
    }
#endif
}
