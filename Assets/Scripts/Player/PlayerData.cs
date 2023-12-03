using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public MovementData MovementData;
}

[System.Serializable]
public struct MovementData
{
    public float MovementSpeed;
    public float AlignmentSpeed;
    public float AlignmentAngularSpeed;
}
