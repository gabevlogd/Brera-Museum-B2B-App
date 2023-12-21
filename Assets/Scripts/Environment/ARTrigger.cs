using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is responsible for activating the AR system based on user input. (see in the Explore state class the CheckForWorldInteraction() method)
/// </summary>
public class ARTrigger : MonoBehaviour
{
    /// <summary>
    /// Event called when the player back in museum scene after he completed the last puzzle
    /// </summary>
    public static event Action LastPuzzleCompleted;
    public GameObject ARSystem;
    public Transform TargetWaypoint;
    [SerializeField]
    private GameObject m_TargetPictureInfoTrigger;
    [SerializeField]
    private GameObject m_PictureA;
    [SerializeField]
    private GameObject m_PictureB;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(Constants.PUZZLE_FOUR) == 1)
        {
            LastPuzzleCompleted?.Invoke();
            m_PictureB.SetActive(true);
            gameObject.SetActive(false);
            m_TargetPictureInfoTrigger.SetActive(true);
        }
        else m_PictureA.SetActive(true);
    }
}
