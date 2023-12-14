using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PUZZLETRIGGER : MonoBehaviour
{
    public AnchorPoint targetAnchorPoint;
    public GameObject child;

    private void Awake()
    {
        if (GamePuzzleManager.PUZZLEONE)
        {
            MoveButtonsManager.TargetAnchorPoint = targetAnchorPoint;
            child.SetActive(false);
        }
    }

}
