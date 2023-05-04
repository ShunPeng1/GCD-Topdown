using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [Header("Grid")]
    public GridXY<GridXYCell> WorldGrid;

    [SerializeField] private int Width = 20, Height = 20;
    [SerializeField] private float CellWidthSize = 1f, CellHeightSize = 1f;

    private void Awake()
    {
        WorldGrid = new GridXY<GridXYCell>(Width, Height, CellWidthSize,CellHeightSize, transform.position);
    }
    
    
}
