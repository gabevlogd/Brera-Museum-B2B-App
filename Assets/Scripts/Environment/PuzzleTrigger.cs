using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public int TargetPuzzleSceneIndex;
    public Transform TargetWaypoint;
    private GameObject m_Curtain;
    [SerializeField]
    private GameObject m_TargetPictureInfoTrigger;

    private void Awake()
    {
        m_Curtain = transform.GetChild(0).gameObject;
        switch (TargetPuzzleSceneIndex)
        {
            case 1:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_ONE) == 1)
                {
                    m_Curtain.SetActive(false);
                    m_TargetPictureInfoTrigger.SetActive(true);
                }

                break;
            case 2:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_TWO) == 1)
                {
                    m_Curtain.SetActive(false);
                    m_TargetPictureInfoTrigger.SetActive(true);
                }

                break;
            case 3:
                if (PlayerPrefs.GetInt(Constants.PUZZLE_THREE) == 1)
                {
                    m_Curtain.SetActive(false);
                    m_TargetPictureInfoTrigger.SetActive(true);
                }

                break;
            default:
                break;
        }
    }
}
