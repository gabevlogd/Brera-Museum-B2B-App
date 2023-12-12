using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[HelpURL("https://docs.google.com/document/d/19ul2Q4WqC30DxK_4pVit2KN8ZSH_c0t_A2t46FrgS-c/edit")]
public class DollyTrack : MonoBehaviour
{
    [HideInInspector]
    public CinemachineSmoothPath This;
    [Tooltip("The anchor point to which the first waypoint of the current dolly track must be connected")]
    public AnchorPoint FirstAnchorPoint;
    [Tooltip("The anchor point to which the last waypoint of the current dolly track must be connected")]
    public AnchorPoint SecondAnchorPoint;

    [HideInInspector]
    public MoveButton FirstButton;
    [HideInInspector]
    public MoveButton SecondButton;
}
