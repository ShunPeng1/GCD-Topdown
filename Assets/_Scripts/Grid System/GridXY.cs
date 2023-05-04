using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXY<TCell> where TCell : GridXYCell
{
    public event EventHandler<OnGridValueChangedEventArgs> EOnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int X;
        public int Y;
    }
    
    private int _width, _height;
    private float _cellWidthSize, _cellHeightSize;
    private Vector3 _originPosition;

    private TCell[,] _gridItems;
    
    public GridXY(int width = 100, int height = 100, float cellWidthSize = 1f, float cellHeightSize = 1f, Vector3 originPosition = new Vector3(), Func<GridXY<TCell>, int, int,TCell> initGridCell = null)
    {
        this._width = width;
        this._height = height;
        this._cellHeightSize = cellHeightSize;
        this._cellWidthSize = cellWidthSize;
        this._originPosition = originPosition;
        _gridItems = new TCell[this._width, this._height];
        
        for (int x = 0; x < this._width; x++)
        {
            for (int y = 0; y < this._height; y++)
            {
                _gridItems[x,y] = initGridCell!=null? initGridCell(this, x, y): default;
                Debug.DrawLine(GetWorldPosition(x,y) , GetWorldPosition(x+1,y), Color.red, 10f);
                Debug.DrawLine(GetWorldPosition(x,y) , GetWorldPosition(x,y+1), Color.red, 10f);
            }
        }
    }
    
    public (int , int ) GetXY(Vector3 position)
    {
        int x = Mathf.FloorToInt((position - _originPosition).x / _cellWidthSize);
        int y = Mathf.FloorToInt((position - _originPosition).z / _cellHeightSize);
        return (x,y);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x * _cellWidthSize, 0, y * _cellHeightSize) + _originPosition;
    }

    public void SetCell(TCell item, int xIndex, int yIndex)
    {
        if (xIndex < _width && xIndex >= 0 && yIndex < _height && yIndex >= 0)
        {
            _gridItems[xIndex, yIndex] = item;
            TriggerGridObjectChanged(xIndex, yIndex);
        }
    }

    public void TriggerGridObjectChanged(int xIndex, int yIndex)
    {
        if( EOnGridValueChanged != null) EOnGridValueChanged(this, new OnGridValueChangedEventArgs{X = xIndex, Y = yIndex});

    }
    
    public void SetCell(TCell item, Vector3 position)
    {
        (int xIndex, int yIndex) = GetXY(position);
        if(xIndex<_width && xIndex >=0 && yIndex < _height && yIndex >= 0)
        {
            _gridItems[xIndex, yIndex] = item;
        };
    }
    
    public TCell GetCell(int xIndex, int yIndex)
    {
        if(xIndex<_width && xIndex >=0 && yIndex < _height && yIndex >= 0) return _gridItems[xIndex, yIndex];
        return default(TCell);
    }
    
    public TCell GetCell(Vector3 position)
    {
        (int xIndex, int yIndex) = GetXY(position);
        if(xIndex<_width && xIndex >=0 && yIndex < _height && yIndex >= 0)
        {
            return _gridItems[xIndex, yIndex];
        }
        return default(TCell);
    }

    public (int , int ) GetWidthHeight()
    {
        return (_width, _height);
    }
    
}
