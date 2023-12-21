using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>
/// This class, attached to the move buttons icons around the map,  is responsible for trigger the movement of the player (see in the Explore state class the CheckForWorldInteraction() method)
/// </summary>
public class MoveButton : MonoBehaviour
{
    /// <summary>
    /// Movement track to which the button refers
    /// </summary>
    public CinemachineSmoothPath Track;
    /// <summary>
    /// direction of travel of the track
    /// </summary>
    public TrackDirection Direction;

    private Transform m_PlayerTransform;

    private void Awake() => m_PlayerTransform = m_PlayerTransform == null ? FindObjectOfType<PlayerData>().transform : m_PlayerTransform;

    private void OnEnable() => AlignWithPlayer();

    private void AlignWithPlayer()
    {
        transform.rotation = Quaternion.identity;
        Quaternion targetRotation = Quaternion.LookRotation(m_PlayerTransform.position - transform.position, transform.up);
        transform.rotation = targetRotation;
    }

    public void TriggerButton() => DollyCartManager.SetDollyCart(Track, Direction);
}
