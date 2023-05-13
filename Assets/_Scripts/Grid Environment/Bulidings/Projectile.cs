using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // speed of the projectile
    [SerializeField] private int _damage = 1; // damage dealt to the target
    [SerializeField] private float _lifeTime = 2f; // time before the projectile disappears
    private Vector2 _targetPosition; // position of the target
    private Transform _target; // reference to the target transform

    public void SetTarget(Transform target)
    {
        _target = target;
        _targetPosition = _target.position;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject); // destroy the projectile if the target is null
            return;
        }

        Vector2 direction = _targetPosition - (Vector2) transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;
        

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_target == null) return;

        _target.GetComponent<EnvironmentHealth>().TakeDamage(_damage); // apply damage to the target
        Destroy(gameObject); // destroy the projectile
    }
    

    private void Start()
    {
        Destroy(gameObject, _lifeTime); // destroy the projectile after a certain time
    }
}
