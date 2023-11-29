using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node 
{
    private int m_X;
    private int m_Y;
    private NodeType m_NodeType;
    private bool m_Walkable;

    public int X { get => m_X;}
    public int Y { get => m_Y;}
    public bool Walkable { get => m_Walkable; set => m_Walkable = value; }
    public NodeType NodeType { get => m_NodeType;}

    public Node(int x, int y)
    {
        m_X = x;
        m_Y = y;
        m_NodeType = NodeType.Normal;
        m_Walkable = true;
    }

    public void SetNode(NodeType type, bool isWalkable)
    {
        m_NodeType = type;
        m_Walkable = isWalkable;
    }
}

public enum NodeType
{
    Start,
    Normal,
    End
}
