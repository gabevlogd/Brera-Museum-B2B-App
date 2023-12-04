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
    public float MovementSpeed;
    public float AlignmentSpeed;
    public float AlignmentAngularSpeed;
}

[System.Serializable]
public struct SightMovementData
{
    [Range(0f, 20f)]
    public float ZoomSens;
    [Range(60f, 100f)]
    public float DefaultZoom;
    [Range(5f, 55f)]
    public float MinZoom;
}
