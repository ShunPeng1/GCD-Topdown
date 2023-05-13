using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBuilding : BuildingBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private List<Transform> _enemiesTransforms;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _fireAngle = 45f;
    [SerializeField] private float _gravity = 9.8f;
    
    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1.0f; // Time between attacks
    [SerializeField] private float timeSinceLastAttack = 0.0f; // Time since last attack

    void Update()
    {
        // Check if enough time has passed since last attack
        if (timeSinceLastAttack < attackCooldown)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else
        {
            // Perform attack and reset timer
            if(_enemiesTransforms.Count > 0) FireProjectile();
            timeSinceLastAttack = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            _enemiesTransforms.Add(other.gameObject.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            _enemiesTransforms.Remove(other.gameObject.transform);
        }
    }

    private void FireProjectile()
    {
        // Create a new instance of the projectile prefab at the fire point
        GameObject projectile = Instantiate(_projectilePrefab,_firePoint.position, Quaternion.identity);

        // Calculate the velocity of the projectile based on the fire angle and speed
        float radianAngle = _fireAngle * Mathf.Deg2Rad;
        float xSpeed = _projectileSpeed * Mathf.Cos(radianAngle);
        float ySpeed = _projectileSpeed * Mathf.Sin(radianAngle);

        // Set the initial velocity of the projectile
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.velocity = new Vector2(xSpeed, ySpeed);

        // Apply gravity to the projectile using a parabolic motion formula
        float timeToTarget = (2f * ySpeed) / _gravity;
        StartCoroutine(ApplyGravity(projectileRigidbody, timeToTarget));
    }

    private IEnumerator ApplyGravity(Rigidbody2D projectileRigidbody, float timeToTarget)
    {
        float elapsedTime = 0f;
        float startTime = Time.time;

        while (elapsedTime < timeToTarget)
        {
            elapsedTime = Time.time - startTime;

            // Calculate the vertical displacement due to gravity
            float verticalDisplacement = 0.5f * _gravity * Mathf.Pow(elapsedTime, 2);

            // Apply the vertical displacement to the projectile's position
            Vector3 newPosition = projectileRigidbody.position;
            newPosition.y -= verticalDisplacement;
            projectileRigidbody.MovePosition(newPosition);

            yield return null;
        }
    }
}
