using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// This class represents the anchor point where player can explore the area moving the view
/// </summary>
public class AnchorPoint : MonoBehaviour
{
    /// <summary>
    /// List of move buttons that must be actives when the player is on the anchor point
    /// </summary>
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
