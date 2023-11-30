using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTracksManager : MonoBehaviour
{
    [HideInInspector]
    public DollyTrack[] TracksList;
    [HideInInspector]
    public CinemachineDollyCart DollyCart;

    private void Awake()
    {
        TracksList = GetTrackList();
        DollyCart = GetDollyCart();
    }

    private DollyTrack[] GetTrackList() => FindObjectsByType<DollyTrack>(FindObjectsSortMode.None);
    private CinemachineDollyCart GetDollyCart() => FindObjectOfType<CinemachineDollyCart>();
}
