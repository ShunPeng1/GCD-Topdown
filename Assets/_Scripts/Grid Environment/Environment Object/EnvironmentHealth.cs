using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnvironmentBehaviour))]
public class EnvironmentHealth : MonoBehaviour
{
    [SerializeField] private float _maxHitPoint;
    [SerializeField] private HealthBarCanvas _healthBarCanvas;

    private EnvironmentBehaviour _environmentBehaviour;
    private float _currHitPoint;    
    
    void Start()
    {
        _environmentBehaviour = GetComponent<EnvironmentBehaviour>();
        _currHitPoint = _maxHitPoint;
    }

    private void Update()
    {
        //TEST 
        if(Input.GetKeyDown(KeyCode.Q))
            TakeDamage(1);
    }

    public void TakeDamage(float _value) {
        _currHitPoint = Mathf.Clamp(_currHitPoint - _value, 0, _maxHitPoint);
        _healthBarCanvas.SetHealth(_currHitPoint, _maxHitPoint);
        if (_currHitPoint <= 0) {
            Die();
        }
    }
    
    private void Die() {
        GetComponent<EnvironmentLoot>()?.InstantiateCollectibles(transform.position);
        Destroy(gameObject);
    }
}
