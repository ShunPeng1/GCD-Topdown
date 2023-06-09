using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXYGameObject : MonoBehaviour
{
    [Header("Grid Properties")]
    [HideInInspector] public GridXY<GridXYCell> GridXY;
    [HideInInspector] public GridXYCell LowerLeftPivotCell;
    [SerializeField] private bool _startInsertGameObjectToGridCell = true;
    [Tooltip("The pivot is lower left")] public Vector2Int GridSize = Vector2Int.one;
    
    public bool IsObstacle = true;

    protected virtual void Start()
    {
        GridXY = GridManager.Instance.WorldGrid;
        LowerLeftPivotCell = GridXY.GetCell(transform.position);
        
        if(!_startInsertGameObjectToGridCell) return;
        
        SetObstacle(true);
    }

    void SetObstacle(bool isObstacle)
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                GridXY.SetGridGameObject(this, LowerLeftPivotCell.XIndex + x, LowerLeftPivotCell.YIndex + y, IsObstacle);
            }
        }
    }

    private void OnDestroy()
    {
        SetObstacle(false);
    }

    protected void OnValidate()
    {
        GridSize.x = Mathf.Max(1, GridSize.x);
        GridSize.y = Mathf.Max(1, GridSize.y);
    }
}
