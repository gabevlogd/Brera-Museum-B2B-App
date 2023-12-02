using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyCartManager : MonoBehaviour
{
    private static DollyTrack[] _tracksList; //forse non servono devo ancora capire.
    private static CinemachineDollyCart _dollyCart;
    private static TrackDirection _trackDirection;

    private void Awake()
    {
        _tracksList = GetTrackList();
        _dollyCart = GetDollyCart();
    }

    private DollyTrack[] GetTrackList() => FindObjectsByType<DollyTrack>(FindObjectsSortMode.None);
    private CinemachineDollyCart GetDollyCart() => FindObjectOfType<CinemachineDollyCart>();

    public static void SetDollyCart(CinemachineSmoothPath targetTrack, TrackDirection trackDirection)
    {
        _dollyCart.m_Path = targetTrack;
        _trackDirection = trackDirection;
        if (trackDirection == TrackDirection.Forward) _dollyCart.m_Position = 0f;
        else _dollyCart.m_Position = targetTrack.PathLength;
    }

    /// <summary>
    /// Start the motion of the dolly cart
    /// </summary>
    public static void StartCartMovement(float cartSpeed)
    {
        if (_trackDirection == TrackDirection.Forward) _dollyCart.m_Speed = cartSpeed;
        else _dollyCart.m_Speed = -cartSpeed;
    }

    /// <summary>
    /// Stop the motion of the dolly cart
    /// </summary>
    public static void ResetCart() => _dollyCart.m_Speed = 0f;

    public static Vector3 GetCartPosition() => _dollyCart.transform.position;
    public static Quaternion GetCartRotation()
    {
        if (_trackDirection == TrackDirection.Forward) return _dollyCart.transform.rotation;
        else return Quaternion.LookRotation(-_dollyCart.transform.forward, _dollyCart.transform.up);
    }
}

public enum TrackDirection
{
    Forward,
    Backward
}
