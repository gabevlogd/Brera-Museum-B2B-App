using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PuzzleManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform m_NextPosition;

    [Header("Puzzle")]
    public PuzzleData AssetData;
    private Camera m_Camera;
    private LineRenderer m_LineRenderer;
    private TouchScreen m_Input;
    private Node m_LastValidNode;
    private Node m_ActualStartNode;
    private Vector3 m_WorldTouchPosition;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Camera = Camera.main;
        m_Input = new TouchScreen();
        m_Input.Enable();
        m_ActualStartNode = default;

        AssetData.Grid = new Grid<Node>(AssetData.GridWidth, AssetData.GridHeight, 1, transform.position, (int x, int y) => new Node(x, y));

        for (int y = 0; y < AssetData.GridHeight; y++)
        {
            for (int x = 0; x < AssetData.GridWidth; x++)
            {
                Vector2Int tmp = new Vector2Int(x, y);

                if (AssetData.StartingPoints.Contains(tmp))
                {
                    AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Start, true);
                    m_LastValidNode = AssetData.Grid.GetGridObject(x, y);
                }
                else if (AssetData.EndingPoints.Contains(tmp))
                    AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.End, true);
                else if (AssetData.CollectiblePoint.Contains(tmp))
                    AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Collectible, true);
                else
                    AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Normal, AssetData.WalkableArray[x].List[y]);
            }
        }

        AssetData.Grid.SetCellSize(GetGridCellSize());
        AssetData.Grid.SetGridOrigin(GetGridOrigin());
    }

    private void Update()
    {
        if (m_Input.PuzzleActions.TouchPos.WasPerformedThisFrame())
        {
            m_WorldTouchPosition = GetScreenToWorld(m_Input.PuzzleActions.TouchPos.ReadValue<Vector2>());
            UpdateLineRenderer(m_WorldTouchPosition);
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        if (!Application.isPlaying)
        {
            AssetData.Grid = new Grid<Node>(AssetData.GridWidth, AssetData.GridHeight, 1, transform.position, (int x, int y) => new Node(x, y));

            for (int y = 0; y < AssetData.GridHeight; y++)
            {
                for (int x = 0; x < AssetData.GridWidth; x++)
                {
                    Vector2Int tmp = new Vector2Int(x, y);

                    if (AssetData.StartingPoints.Contains(tmp))
                    {
                        AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Start, true);
                    }
                    else if (AssetData.EndingPoints.Contains(tmp))
                        AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.End, true);
                    else if (AssetData.CollectiblePoint.Contains(tmp))
                        AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Collectible, true);
                    else
                        AssetData.Grid.GetRefGridObject(x, y).SetNode(NodeType.Normal, AssetData.WalkableArray[x].List[y]);
                }
            }

            AssetData.Grid.SetCellSize(GetGridCellSize());
            AssetData.Grid.SetGridOrigin(GetGridOrigin());
        }

        for (int i = 0; i < AssetData.GridWidth; i++)
        {
            for (int j = 0; j < AssetData.GridHeight; j++)
            {   
                //Debug.Log("Node type: " + AssetData.Grid.GetGridObject(i, j).NodeType.ToString());

                if(AssetData.Grid.GetGridObject(i, j).NodeType == NodeType.Start)
                    Gizmos.color = Color.black;
                else if (AssetData.Grid.GetGridObject(i, j).NodeType == NodeType.End)
                    Gizmos.color = Color.white;
                else if (CheckPointInCollectibles(new Vector2Int(i, j)))
                    Gizmos.color = Color.blue;
                else if (AssetData.Grid.GetGridObject(i, j).Walkable)
                    Gizmos.color = Color.green;
                else Gizmos.color = Color.red;

                Gizmos.DrawCube(AssetData.Grid.GetWorldPosition(i, j), new Vector3(AssetData.Grid.GetCellSize(), AssetData.Grid.GetCellSize(), 0.2f));
            }
        }
    }

    private void UpdateLineRenderer(Vector3 position)
    {
        Node newNode = AssetData.Grid.GetGridObject(position);

        if (!newNode.Walkable) return;
        if (newNode.NodeType == NodeType.Start)
        {
            if (m_LineRenderer.positionCount == 0)
            {
                m_LineRenderer.positionCount = 1;
                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, position);
                m_LastValidNode = newNode;
                m_ActualStartNode = newNode;
                return;
            }
            else if (m_LineRenderer.positionCount > 0 && newNode.Equals(m_ActualStartNode))
            {
                m_LineRenderer.positionCount = 0;
                m_ActualStartNode = default;
                return;
            }
        }
        if (m_LineRenderer.positionCount > 0)
        {
            //avoids the possibility of going obliquely
            if (newNode.X != m_LastValidNode.X && newNode.Y != m_LastValidNode.Y) return;
            //checks if newNode is not next to the last one (only horizontally or vertically)
            if (Mathf.Abs(newNode.X - m_LastValidNode.X) > 1 || Mathf.Abs(newNode.Y - m_LastValidNode.Y) > 1) return;
            //checks if the newNode is the same of the last one
            if (m_LineRenderer.positionCount > 0 && newNode.X == m_LastValidNode.X && newNode.Y == m_LastValidNode.Y) return;
            if (m_LineRenderer.positionCount > 1)
            {
                //gets the coordinates of the second-last one node
                AssetData.Grid.GetXY(m_LineRenderer.GetPosition(m_LineRenderer.positionCount - 2), out int x, out int y);
                //checks if the newNode is the same of the second-last one
                if (newNode.X == x && newNode.Y == y)
                {
                    m_LineRenderer.positionCount--;
                    m_LastValidNode = newNode;
                    return;
                }
            }
            //checks if the newNode is already in the positions list of line renderer
            for (int i = 0; i < m_LineRenderer.positionCount; i++)
                if (AssetData.Grid.GetWorldPosition(newNode.X, newNode.Y) == m_LineRenderer.GetPosition(i))
                    return;


            m_LineRenderer.positionCount++;
            m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, position);
            m_LastValidNode = newNode;

            CheckEndPuzzle(m_LineRenderer.GetPosition(m_LineRenderer.positionCount - 1));
        }

    }

    private Vector3 GetScreenToWorld(Vector2 screenPos)
    {
        Vector3 worldPos = m_Camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(m_Camera.transform.position.z)));
        AssetData.Grid.GetXY(worldPos, out int x, out int y);
        return AssetData.Grid.GetWorldPosition(x, y);
    }

    private float GetGridCellSize()
    {
        float cellSize = 0f;

        if (transform.localScale.x < transform.localScale.y)
        {
            cellSize = transform.localScale.x / AssetData.GridWidth;
        }
        else
        {
            cellSize = transform.localScale.y / AssetData.GridHeight;
        }

        return cellSize;
    }

    private Vector3 GetGridOrigin() => new Vector3(transform.position.x - GetGridCellSize() * AssetData.GridWidth * 0.5f, transform.position.y - GetGridCellSize() * AssetData.GridHeight * 0.5f, 0f);

    private void CheckEndPuzzle(Vector3 pos)
    {
        if (AssetData.Grid.GetGridObject(pos).NodeType == NodeType.End)
        {
            if(GetCountActualCollectiblePoints() == AssetData.CollectiblePoint.Count)
            {
                if (GamePuzzleManager.instance == null)
                    Debug.Log("PuzzleCompleted");
                else
                {
                    Vector3 nextPos;
                    if (m_NextPosition != null)
                        nextPos = m_NextPosition.position;
                    else
                        nextPos = Vector3.zero;

                    GamePuzzleManager.instance.EventManager.TriggerEvent(Constants.SINGLE_PUZZLE_COMPLETED, nextPos);
                }
            }
        }
    }

    private int GetCountActualCollectiblePoints()
    {
        int count = 0;

        for (int i = 0; i < m_LineRenderer.positionCount; i++)
        {
            AssetData.Grid.GetXY(m_LineRenderer.GetPosition(i), out int x, out int y);
            Vector2Int tmp = new Vector2Int(x, y);
            if (CheckPointInCollectibles(tmp))
                count++;
        }

        return count;
    }

    private bool CheckPointInCollectibles(Vector2Int pos)
    {
        if (AssetData.CollectiblePoint.Contains(pos)) return true;
        return false;
    }

    private bool CanUpdateRenderer(Vector3 position)
    {
        if (m_LineRenderer.positionCount > 0) return true;
        Node currentNode = AssetData.Grid.GetGridObject(position);
        if (currentNode.NodeType == NodeType.Start)
        {
            m_LineRenderer.positionCount = 1;
            m_LineRenderer.SetPosition(0, AssetData.Grid.GetWorldPosition(currentNode.X, currentNode.Y));
            m_LastValidNode = currentNode;
            return true;
        }
        return false;
    }
}
