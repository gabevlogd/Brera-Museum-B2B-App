using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// This class is responsible for the managing of the dolly cart that moves on the dolly tracks around the map
/// </summary>
public class DollyCartManager : MonoBehaviour
{
    private static CinemachineDollyCart m_DollyCart;
    private static TrackDirection m_TrackDirection;

    private void Awake() => m_DollyCart = GetDollyCart();

    private CinemachineDollyCart GetDollyCart() => FindObjectOfType<CinemachineDollyCart>();


    /// <summary>
    /// Set the starting condition of the dolly cart
    /// </summary>
    /// <param name="targetTrack">The track upon which move</param>
    /// <param name="trackDirection">The moving direction on the track</param>
    public static void SetDollyCart(CinemachineSmoothPath targetTrack, TrackDirection trackDirection)
    {
        m_DollyCart.m_Path = targetTrack;
        m_TrackDirection = trackDirection;
        if (trackDirection == TrackDirection.Forward) m_DollyCart.m_Position = 0f;
        else m_DollyCart.m_Position = targetTrack.PathLength;
    }

    /// <summary>
    /// Start the motion of the dolly cart
    /// </summary>
    public static void StartCartMovement(float cartSpeed)
    {
        if (m_TrackDirection == TrackDirection.Forward) m_DollyCart.m_Speed = cartSpeed;
        else m_DollyCart.m_Speed = -cartSpeed;
    }

    /// <summary>
    /// Stop the motion of the dolly cart
    /// </summary>
    public static void ResetCart() => m_DollyCart.m_Speed = 0f;

    public static Vector3 GetCartPosition() => m_DollyCart.transform.position;
    public static Quaternion GetCartRotation()
    {
        if (m_TrackDirection == TrackDirection.Forward) return m_DollyCart.transform.rotation;
        else return Quaternion.LookRotation(-m_DollyCart.transform.forward, m_DollyCart.transform.up);
    }
}

public enum TrackDirection
{
    Forward,
    Backward
}
