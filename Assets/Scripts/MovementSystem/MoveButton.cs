using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveButton : MonoBehaviour
{
    public CinemachineSmoothPath Track;
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
