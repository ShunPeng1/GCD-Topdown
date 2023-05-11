using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GridXY<GridXYCell> _gridXY;
    [SerializeField] private Transform Map;

    private EnvironmentBehaviour _holdingEnvironmentBehaviour;
    private int _holdingPivotX, _holdingPivotY;
    private bool _isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        _gridXY = GridManager.Instance.WorldGrid;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHolding)
        {
            SetHoldingPivotXY();
            AdjustToGrid();

            if (Input.GetKeyUp(KeyCode.F))
            {
                if (CheckValidGridPosition())
                {
                    PlaceBuilding();
                    
                    _isHolding = false;
                }
                else
                {
                    Debug.Log("Fail Placing");
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                HoldBuilding();
                _isHolding = true;
                
            }
        }
    }

    void SetHoldingPivotXY()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        (_holdingPivotX, _holdingPivotY) = _gridXY.GetXY(mousePosition);
    }

    void HoldBuilding()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _holdingEnvironmentBehaviour = Instantiate(ResourceManager.Instance.BombTower, mousePosition, Quaternion.identity,
            transform);
        _holdingEnvironmentBehaviour.GetComponent<Collider2D>().enabled = false;

    }
    void AdjustToGrid()
    {
        _holdingEnvironmentBehaviour.transform.position = _gridXY.GetWorldPosition(_holdingPivotX, _holdingPivotY);
    }

    private bool CheckValidGridPosition()
    {
        for (int x = 0; x < _holdingEnvironmentBehaviour.GridSize.x; x++)
        {
            for (int y = 0; y < _holdingEnvironmentBehaviour.GridSize.y; y++)
            {
                if (_gridXY.GetCellObstacle(_holdingPivotX + x, _holdingPivotY + y))
                {
                    return false;
                }
            }
        }
        return true;
    }

    void PlaceBuilding()
    {
        for (int x = 0; x < _holdingEnvironmentBehaviour.GridSize.x; x++)
        {
            for (int y = 0; y < _holdingEnvironmentBehaviour.GridSize.y; y++)
            {
                _gridXY.SetGridGameObject(_holdingEnvironmentBehaviour, _holdingPivotX + x, _holdingPivotY + y,
                    _holdingEnvironmentBehaviour.IsObstacle);
            }
        }
        
        _holdingEnvironmentBehaviour.GetComponent<Collider2D>().enabled = true;

    }
}