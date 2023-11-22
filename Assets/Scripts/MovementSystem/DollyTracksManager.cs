using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTracksManager : MonoBehaviour
{
    public CinemachineSmoothPath[] TracksList;
    public CinemachineDollyCart DollyCart;

    private void Awake()
    {
        TracksList = FindObjectsByType<CinemachineSmoothPath>(FindObjectsSortMode.None);
    }
}
