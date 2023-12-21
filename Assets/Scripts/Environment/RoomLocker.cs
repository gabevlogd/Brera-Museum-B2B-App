using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// This class is responsible for locking the fourth room until the player resolve the first three puzzles
/// </summary>
public class RoomLocker : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Event called when player try to move to the locked room when is still locked
    /// </summary>
    public static event Action RoomLockedMessage;
    /// <summary>
    /// Event called when the locke room becomes unlocked
    /// </summary>
    public static event Action RoomUnlocked;
    [SerializeField]
    private Transform TargetWaypoint;
    private Transform m_PlayerTransform;

    private void Awake()
    {
        if (CanUnlockRoom())
        {
            RoomUnlocked?.Invoke();
            gameObject.SetActive(false);
            return;
        }

        m_PlayerTransform = FindFirstObjectByType<PlayerData>().transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_PlayerTransform.position != TargetWaypoint.transform.position) return;
        RoomLockedMessage?.Invoke();
    }

    private bool CanUnlockRoom() => PlayerPrefs.GetInt(Constants.PUZZLE_ONE) == 1 && PlayerPrefs.GetInt(Constants.PUZZLE_TWO) == 1 && PlayerPrefs.GetInt(Constants.PUZZLE_THREE) == 1;

}
