using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool pref1;
    public bool pref2;
    public bool pref3;
    public bool pref4;
    public bool unlockRoomFour;
    public RoomLocker RoomLocker;

    private void Update()
    {
        if (unlockRoomFour)
        {
            RoomLocker.gameObject.SetActive(false);
            unlockRoomFour = false;
        }

        if (pref1)
        {
            PlayerPrefs.SetInt(Constants.PUZZLE_ONE, 1);
            pref1 = false;
        }

        if (pref2)
        {
            PlayerPrefs.SetInt(Constants.PUZZLE_TWO, 1);
            pref2 = false;
        }

        if (pref3)
        {
            PlayerPrefs.SetInt(Constants.PUZZLE_THREE, 1);
            pref3 = false;
        }

        if (pref4)
        {
            PlayerPrefs.SetInt(Constants.PUZZLE_FOUR, 1);
            pref4 = false;
        }
    }
}
