using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDebugger : MonoBehaviour
{
    public PlayerController PlayerSM;
    public CinemachineSmoothPath TargetTrack;
    public TrackDirection TrackDirection;

    private void Update()
    {
        if (TargetTrack != null)
        {
            DollyCartManager.SetDollyCart(TargetTrack, TrackDirection);
            TargetTrack = null;
            PlayerSM.m_StateMachine.ChangeState(PlayerSM.Move);
        }
    }
}
