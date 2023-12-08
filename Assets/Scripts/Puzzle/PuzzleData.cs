using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewPuzzleData", menuName = "ScriptableObject/Puzzle")]
public class PuzzleData : ScriptableObject
{
    public Grid<Node> Grid;
    [SerializeField] public int GridWidth;
    [SerializeField] public int GridHeight;
    [SerializeField] public List<Vector2Int> StartingPoints = new();
    [SerializeField] public List<Vector2Int> EndingPoints = new();
    [SerializeField] public List<Vector2Int> CollectiblePoint = new();
    [SerializeField] public List<ListWrapper> WalkableArray = new();
}

[System.Serializable] public class ListWrapper { public List<bool> List; } 
