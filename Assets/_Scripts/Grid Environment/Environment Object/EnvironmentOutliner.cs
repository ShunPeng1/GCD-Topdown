using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnvironmentBehaviour))]
public class EnvironmentOutliner : MonoBehaviour
{
    
    [SerializeField] private GameObject _outline;
    private EnvironmentBehaviour _environmentBehaviour;
    void Start()
    {
        _environmentBehaviour = GetComponent<EnvironmentBehaviour>();

        if (_outline == null)
        {
            _outline = Instantiate(ResourceManager.Instance.EnvironmentOutline, transform);
            _outline.transform.localScale = (Vector2) _environmentBehaviour.GridSize;
        }
        
        _outline.SetActive(false);
    }

    private void OnMouseEnter() {
        _outline.SetActive(true);

    }

    private void OnMouseExit() {
        _outline.SetActive(false);
    }

}
