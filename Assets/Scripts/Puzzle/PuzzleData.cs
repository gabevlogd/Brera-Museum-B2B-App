using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzleData", menuName = "ScriptableObject/Puzzle")]
public class PuzzleData : ScriptableObject
{
    public Grid<Node> Grid;
    public int GridWidth;
    public int GridHeight;
    //public float CellSize;
    //public Vector3 PuzzlePosition;

}
