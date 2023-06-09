using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityUtilities;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [Header("Grid")]
    public GridXY<GridXYCell> WorldGrid;

    [SerializeField] private int Width = 20, Height = 20;
    [SerializeField] private float CellWidthSize = 1f, CellHeightSize = 1f;

    [Header("Tile map")]
    [SerializeField] Tilemap _collisionTilemap;
    private void Awake()
    {
        WorldGrid = new GridXY<GridXYCell>(Width, Height, CellWidthSize,CellHeightSize, transform.position);
        
        // Add Collider from static Tilemap
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++) 
            {
                TileBase tileBase =  _collisionTilemap.GetTile(Vector3Int.FloorToInt(WorldGrid.GetWorldPosition(x,y)));
                if (tileBase != null)
                {
                    WorldGrid.SetCellObstacle(x,y, true);
                    Debug.Log("Cell" +x+" "+y+" have collider");
                }
            }
        }
    }
    
    
}
