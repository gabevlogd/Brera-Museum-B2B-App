using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public MovementData MovementData;
    [SerializeField]
    public SightMovementData SightMovementData;
        
}

[System.Serializable]
public struct MovementData
{
    [Range(0.1f, 10f)]
    public float MovementSpeed;
    [Tooltip("alignment speed of the player with the position of the current anchor point")]
    [Range(0.1f, 10f)]
    public float AlignmentSpeed;
    [Tooltip("alignment angular speed of the player with the roation of the current anchor point")]
    [Range(1f, 40f)]
    public float AlignmentAngularSpeed;
}

[System.Serializable]
public struct SightMovementData
{
    [Header("Zoom Data")]
    [Range(0f, 20f)]
    public float ZoomSens;
    [Tooltip("Starting value of the zoom")]
    [Range(60f, 100f)]
    public float DefaultZoom;
    [Tooltip("Minimum value of the zoom")]
    [Range(5f, 55f)]
    public float MinZoom;
    [Header("Rotation Data")]
    [Range(0f, 500f)]
    public float YawSens;
    [Range(0f, 500f)]
    public float PitchSens;
    [Tooltip("Maximum degrees of pitch rotation")]
    [Range(10f, 90f)]
    public float MaxPitch;
    [Tooltip("Minimum degrees of pitch rotation")]
    [Range(-10f, -90f)]
    public float MinPitch;
    [Range(0.1f, 4f)]
    public float AngularDeceleration;

}
