using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzleData", menuName = "ScriptableObject/Puzzle")]
public class PuzzleData : ScriptableObject
{
    [SerializeField] public Grid<Node> Grid;
    [SerializeField] public int GridWidth;
    [SerializeField] public int GridHeight;
    //public float CellSize;
    //public Vector3 PuzzlePosition;

}
