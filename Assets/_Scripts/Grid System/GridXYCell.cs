using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXYCell
{
    [Header("Base")]
    private readonly GridXY<GridXYCell> _gridXY;
    public int XIndex;
    public int YIndex;
    private int _a;
    public List<GridXYCell> AdjacentItems { get; private set; } = new();
    public GridXYGameObject GridGameObject { get; private set; }
    public bool IsObstacle { get; set; }

    [Header("A Star Pathfinding")] 
    public GridXYCell ParentXYCell = null; 
    public int FCost;
    public int HCost;
    public int GCost;
    
    
    public GridXYCell(GridXY<GridXYCell> grid, int x, int y, bool isObstacle = false, GridXYGameObject gridGameObject = null)
    {
        _gridXY = grid;
        XIndex = x;
        YIndex = y;
        IsObstacle = isObstacle;
        GridGameObject = gridGameObject;
    }

    public void SetAdjacencyCell()
    {
        GridXYCell[] adjacentRawItems =
        {
            _gridXY.GetCell(XIndex + 1, YIndex),
            _gridXY.GetCell(XIndex - 1, YIndex),
            _gridXY.GetCell(XIndex, YIndex + 1),
            _gridXY.GetCell(XIndex, YIndex - 1)
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
        return (second.XIndex - first.XIndex , second.YIndex-first.YIndex);
    }
    
    public static (int xDiff, int yDiff) GetIndexDifferenceAbsolute(GridXYCell first, GridXYCell second)
    {
        return (Mathf.Abs(second.XIndex - first.XIndex), Mathf.Abs(second.YIndex - first.YIndex));
    }
}
