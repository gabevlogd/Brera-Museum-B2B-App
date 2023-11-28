using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleData Data;
    private Camera _camera;
    private LineRenderer _lineRenderer;
    private TouchScreen _input;
    private Node _lastValidNode;
    private Vector3 _worldTouchPosition;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
        Data.Grid = new Grid<Node>(Data.GridWidth, Data.GridHeight, GetGridCellSize(), GetGridOrigin(), (int x, int y) => new Node(x, y));
        _input = new TouchScreen();
        _input.Enable();

    }

    private void Update()
    {
        _worldTouchPosition = GetScreenToWorld(_input.PuzzleActions.TouchPos.ReadValue<Vector2>());
        UpdateLineRenderer(_worldTouchPosition);

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            float cellSize = GetGridCellSize();
            for (int row = 0; row < Data.GridWidth; row++)
            {
                for (int column = 0; column < Data.GridHeight; column++)
                {
                    if (Data.Grid.GetGridObject(row, column).Walkable)
                        Gizmos.color = Color.green;
                    else Gizmos.color = Color.red;

                    Gizmos.DrawCube(Data.Grid.GetWorldPosition(row, column), new Vector3(cellSize, cellSize, 0.2f));
                }
            }
        }
        
    }

    private void UpdateLineRenderer(Vector3 position)
    {
        Node newNode = Data.Grid.GetGridObject(position);

        if (!newNode.Walkable) return;
        //avoids the possibility of going obliquely
        if (newNode.x != _lastValidNode.x && newNode.y != _lastValidNode.y) return;
        //checks if newNode is not next to the last one (only horizontally or vertically)
        if (Mathf.Abs(newNode.x - _lastValidNode.x) > 1 || Mathf.Abs(newNode.y - _lastValidNode.y) > 1) return;
        //checks if the newNode is the same of the last one
        if (_lineRenderer.positionCount > 0 && newNode.x == _lastValidNode.x && newNode.y == _lastValidNode.y) return;
        if (_lineRenderer.positionCount > 1)
        {
            //gets the coordinates of the second-last one node
            Data.Grid.GetXY(_lineRenderer.GetPosition(_lineRenderer.positionCount - 2), out int x, out int y);
            //checks if the newNode is the same of the second-last one
            if (newNode.x == x && newNode.y == y)
            {
                _lineRenderer.positionCount--;
                _lastValidNode = newNode;
                return;
            }
        }
        //checks if the newNode is already in the positions list of line renderer
        for(int i = 0; i < _lineRenderer.positionCount; i++)
            if (Data.Grid.GetWorldPosition(newNode.x, newNode.y) == _lineRenderer.GetPosition(i))
                return;
        

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        _lastValidNode = newNode;
    }

    private Vector3 GetScreenToWorld(Vector2 screenPos)
    {
        Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(_camera.transform.position.z)));
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
        if (_lineRenderer.positionCount > 0) return true;
        Node currentNode = Data.Grid.GetGridObject(position);
        if (currentNode.NodeType == NodeType.Start)
        {
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, Data.Grid.GetWorldPosition(currentNode.x, currentNode.y));
            _lastValidNode = currentNode;
            return true;
        }
        return false;
    }
}
