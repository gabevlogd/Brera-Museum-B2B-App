using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleData AssetData;
    private PuzzleData Data;
    private Camera m_Camera;
    private LineRenderer m_LineRenderer;
    private TouchScreen m_Input;
    private Node m_LastValidNode;
    private Vector3 m_WorldTouchPosition;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Camera = Camera.main;
        m_Input = new TouchScreen();
        m_Input.Enable();

        Data = AssetData;
        Data.Grid = new Grid<Node>(Data.GridWidth, Data.GridHeight, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Node(x, y));

        for (int y = 0; y < Data.GridHeight; y++)
        {
            for (int x = 0; x < Data.GridWidth; x++)
            {
                Vector2 tmp = new Vector2(x, y);

                if (Data.StartingPoint.Contains(tmp))
                {
                    Data.Grid.GetRefGridObject(x, y).SetNode(NodeType.Start, true);
                    m_LastValidNode = Data.Grid.GetGridObject(x, y);
                }
                else if (Data.EndingPoint.Contains(tmp))
                    Data.Grid.GetRefGridObject(x, y).SetNode(NodeType.End, true);
                else
                    Data.Grid.GetRefGridObject(x, y).SetNode(NodeType.Normal, AssetData.WalkableArray[x].List[y]);
            }
        }

        Data.Grid.SetCellSize(GetGridCellSize());
        Data.Grid.SetGridOrigin(GetGridOrigin());
    }

    private void Update()
    {
        m_WorldTouchPosition = GetScreenToWorld(m_Input.PuzzleActions.TouchPos.ReadValue<Vector2>());
        UpdateLineRenderer(m_WorldTouchPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Data = AssetData;
        Data.Grid = new Grid<Node>(Data.GridWidth, Data.GridHeight, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Node(x, y));

        for (int y = 0; y < Data.GridHeight; y++)
        {
            for (int x = 0; x < Data.GridWidth; x++)
            {
                Data.Grid.GetRefGridObject(x, y).SetNode(NodeType.Normal, AssetData.WalkableArray[x].List[y]);
            }
        }

        Data.Grid.SetCellSize(GetGridCellSize());
        Data.Grid.SetGridOrigin(GetGridOrigin());

        for (int i = 0; i < Data.GridWidth; i++)
        {
            for (int j = 0; j < Data.GridHeight; j++)
            {
                Debug.Log("Node type: " + Data.Grid.GetGridObject(i, j).NodeType.ToString());

                if (Data.Grid.GetGridObject(i, j).Walkable)
                    Gizmos.color = Color.green;
                else Gizmos.color = Color.red;

                Gizmos.DrawCube(Data.Grid.GetWorldPosition(i, j), new Vector3(Data.Grid.GetCellSize(), Data.Grid.GetCellSize(), 0.2f));
            }
        }
    }

    private void UpdateLineRenderer(Vector3 position)
    {
        Node newNode = Data.Grid.GetGridObject(position);

        if (!newNode.Walkable) return;
        //avoids the possibility of going obliquely
        if (newNode.X != m_LastValidNode.X && newNode.Y != m_LastValidNode.Y) return;
        //checks if newNode is not next to the last one (only horizontally or vertically)
        if (Mathf.Abs(newNode.X - m_LastValidNode.X) > 1 || Mathf.Abs(newNode.Y - m_LastValidNode.Y) > 1) return;
        //checks if the newNode is the same of the last one
        if (m_LineRenderer.positionCount > 0 && newNode.X == m_LastValidNode.X && newNode.Y == m_LastValidNode.Y) return;
        if (m_LineRenderer.positionCount > 1)
        {
            //gets the coordinates of the second-last one node
            Data.Grid.GetXY(m_LineRenderer.GetPosition(m_LineRenderer.positionCount - 2), out int x, out int y);
            //checks if the newNode is the same of the second-last one
            if (newNode.X == x && newNode.Y == y)
            {
                m_LineRenderer.positionCount--;
                m_LastValidNode = newNode;
                return;
            }
        }
        //checks if the newNode is already in the positions list of line renderer
        for(int i = 0; i < m_LineRenderer.positionCount; i++)
            if (Data.Grid.GetWorldPosition(newNode.X, newNode.Y) == m_LineRenderer.GetPosition(i))
                return;
        

        m_LineRenderer.positionCount++;
        m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, position);
        m_LastValidNode = newNode;
    }

    private Vector3 GetScreenToWorld(Vector2 screenPos)
    {
        Vector3 worldPos = m_Camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(m_Camera.transform.position.z)));
        Data.Grid.GetXY(worldPos, out int x, out int y);
        return Data.Grid.GetWorldPosition(x, y);
    }

    private float GetGridCellSize()
    {
        float cellSize = 0f;

        if (transform.localScale.x < transform.localScale.y)
        {
            cellSize = transform.localScale.x / Data.GridWidth;
        }
        else
        {
            cellSize = transform.localScale.y / Data.GridHeight;
        }

        return cellSize;
    }

    private Vector3 GetGridOrigin() => new Vector3(transform.position.x - GetGridCellSize() * Data.GridWidth * 0.5f, transform.position.y - GetGridCellSize() * Data.GridHeight * 0.5f, 0f);
    

    private bool CanUpdateRenderer(Vector3 position)
    {
        if (m_LineRenderer.positionCount > 0) return true;
        Node currentNode = Data.Grid.GetGridObject(position);
        if (currentNode.NodeType == NodeType.Start)
        {
            m_LineRenderer.positionCount = 1;
            m_LineRenderer.SetPosition(0, Data.Grid.GetWorldPosition(currentNode.X, currentNode.Y));
            m_LastValidNode = currentNode;
            return true;
        }
        return false;
    }
}
