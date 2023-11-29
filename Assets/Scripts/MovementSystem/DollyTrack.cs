using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Cinemachine.CinemachineSmoothPath;

public class DollyTrack : MonoBehaviour
{
    [HideInInspector]
    public int ID;
    //[HideInInspector]
    public CinemachineSmoothPath AnchoredTrack;
    [HideInInspector]
    public CinemachineSmoothPath This;
}
