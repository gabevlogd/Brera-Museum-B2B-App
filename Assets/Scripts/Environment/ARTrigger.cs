using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTrigger : MonoBehaviour
{
    public GameObject ARSystem;
    public Transform TargetWaypoint;
    [SerializeField]
    private GameObject m_PictureA;
    [SerializeField]
    private GameObject m_PictureB;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(Constants.PUZZLE_FOUR) == 1)
            m_PictureB.SetActive(true);
        else m_PictureA.SetActive(true);
    }
}
