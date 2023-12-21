using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PictureInfoTrigger : MonoBehaviour, IPointerClickHandler
{
    public static event Action<string> OpenPictureInfo;
    [SerializeField]
    private int m_TargetPicture;
    private Transform m_PlayerTransform;
    [SerializeField]
    private Transform m_TargetWaypoint;
    private BoxCollider m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
        m_PlayerTransform = FindObjectOfType<PlayerData>().transform;
    }

    private void OnEnable() => HUD.PictureInfoClosed += EnableTrigger;

    private void OnDisable() => HUD.PictureInfoClosed -= EnableTrigger;

    public void OnPointerClick(PointerEventData eventData) => TriggerPictureInfo();

    private void TriggerPictureInfo()
    {
        if (m_PlayerTransform.position != m_TargetWaypoint.position) return;

        SoundManager.Play("Button");

        switch (m_TargetPicture)
        {
            case 1:
                OpenPictureInfo?.Invoke(Constants.PICTURE_INFO_A);
                break;
            case 2:
                OpenPictureInfo?.Invoke(Constants.PICTURE_INFO_B);
                break;
            case 3:
                OpenPictureInfo?.Invoke(Constants.PICTURE_INFO_C);
                break;
            case 4:
                OpenPictureInfo?.Invoke(Constants.PICTURE_INFO_D);
                break;
        }

        DisableTrigger();
    }

    private void EnableTrigger() => m_Collider.enabled = true;
    private void DisableTrigger() => m_Collider.enabled = false;
}
