using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewPuzzleData", menuName = "ScriptableObject/Puzzle")]
public class PuzzleData : ScriptableObject
{
    public Grid<Node> Grid;
    [SerializeField] public int GridWidth;
    [SerializeField] public int GridHeight;
    [SerializeField] public List<Vector2> StartingPoint = new List<Vector2>();
    [SerializeField] public List<Vector2> EndingPoint = new List<Vector2>();
    [SerializeField] public List<ListWrapper> WalkableArray = new List<ListWrapper>();
}

[System.Serializable] public class ListWrapper { public List<bool> List; } 
