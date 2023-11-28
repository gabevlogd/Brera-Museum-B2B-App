using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Node 
{
    public int x;
    public int y;
    public NodeType NodeType;
    public bool Walkable;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        NodeType = NodeType.Normal;
        Walkable = true;
    }
}

public enum NodeType
{
    Start,
    Normal,
    End
}
