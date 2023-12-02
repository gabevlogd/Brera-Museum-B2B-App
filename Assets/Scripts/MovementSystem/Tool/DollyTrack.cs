using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Cinemachine.CinemachineSmoothPath;

[HelpURL("https://docs.google.com/document/d/19ul2Q4WqC30DxK_4pVit2KN8ZSH_c0t_A2t46FrgS-c/edit")]
public class DollyTrack : MonoBehaviour
{
    [HideInInspector]
    public CinemachineSmoothPath This;
    [Tooltip("The dolly track to which the first waypoint of the current dolly track must be connected")]
    public CinemachineSmoothPath FirstWaypointAnchorTrack;
    [Tooltip("the index of the dolly track waypoint entered in the field above to which the current dolly track must be connected")]
    [Min(0)]
    public int FirstWaypointAnchorIndex;
    [Tooltip("The dolly track to which the last waypoint of the current dolly track must be connected")]
    public CinemachineSmoothPath LastWaypointAnchorTrack;
    [Tooltip("the index of the dolly track waypoint entered in the field above to which the current dolly track must be connected")]
    [Min(0)]
    public int LastWaypointAnchorIndex;


}
