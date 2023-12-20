using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool unlockRoomFour;
    public RoomLocker RoomLocker;

    private void Update()
    {
        if (unlockRoomFour)
        {
            RoomLocker.gameObject.SetActive(false);
            unlockRoomFour = false;
        }
    }
}
