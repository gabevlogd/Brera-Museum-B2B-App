using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleData Data;
    private Camera _camera;
    private LineRenderer _lineRenderer;
    private Node _lastValidNode;
    private TouchScreen _input;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
        Data.Grid = new Grid<Node>(Data.GridWidth, Data.GridHeight, Data.CellSize, Data.PuzzlePosition, (int x, int y) => new Node(x, y));
        _input = new TouchScreen();
        _input.Enable();
    }

    private void Update()
    {
        UpdateLineRenderer(GetScreenToWorld(_input.PuzzleActions.TouchPos.ReadValue<Vector2>()));
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
}
