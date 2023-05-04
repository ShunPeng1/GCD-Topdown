using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXYGameObject : MonoBehaviour
{
    protected GridXY<GridXYCell> GridXY;

    protected virtual void Start()
    {
        GridXY = GridManager.Instance.WorldGrid;
        GridXY.SetGridGameObject(this, transform.position);
    }
    
}
