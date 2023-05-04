using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXYCell
{
    [Header("Base")]
    private readonly GridXY<GridXYCell> _gridXY;
    private readonly int _xIndex;
    private readonly int _yIndex;
    private int _a;
    public List<GridXYCell> AdjacentItems { get; private set; } = new();
    public GridXYGameObject GridGameObject { get; private set; }
    public bool IsObstacle { get; private set; }

    [Header("A Star Pathfinding")] 
    public GridXYCell ParentXYCell = null; 
    public int FCost;
    public int HCost;
    public int GCost;
    
    
    public GridXYCell(GridXY<GridXYCell> grid, int x, int y, bool isObstacle = false, GridXYGameObject gridGameObject = null)
    {
        _gridXY = grid;
        _xIndex = x;
        _yIndex = y;
        IsObstacle = isObstacle;
        GridGameObject = gridGameObject;
    }

    public void SetAdjacencyCell()
    {
        GridXYCell[] adjacentRawItems =
        {
            _gridXY.GetCell(_xIndex + 1, _yIndex),
            _gridXY.GetCell(_xIndex - 1, _yIndex),
            _gridXY.GetCell(_xIndex, _yIndex + 1),
            _gridXY.GetCell(_xIndex, _yIndex - 1)
        };

        foreach (var rawItem in adjacentRawItems)
        {
            if (rawItem != null)
            {
                AdjacentItems.Add(rawItem);
            }
        }
        
    }

    public void SetGameObject(GridXYGameObject gameObject, bool isObstacle = false)
    {
        GridGameObject = gameObject;
        IsObstacle = isObstacle;
    }
    
    public static (int xDiff, int yDiff) GetIndexDifference(GridXYCell first, GridXYCell second)
    {
        return (second._xIndex - first._xIndex , second._yIndex-first._yIndex);
    }
    
    public static (int xDiff, int yDiff) GetIndexDifferenceAbsolute(GridXYCell first, GridXYCell second)
    {
        return (Mathf.Abs(second._xIndex - first._xIndex), Mathf.Abs(second._yIndex - first._yIndex));
    }
}
