using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
public class BuildingAttack : MonoBehaviour
{
    [SerializeField] private Transform target; // The current target for the tower
    [SerializeField] private float range = 5.0f; // The range of the tower
    [SerializeField] private float fireRate = 1.0f; // The rate of fire of the tower
    [SerializeField] private float turnSpeed = 10.0f; // The turning speed of the tower
    [SerializeField] private GameObject projectilePrefab; // The prefab of the projectile the tower will fire
    [SerializeField] private Transform firePoint; // The point where the projectile will spawn

    private float fireCountdown = 0.0f; // Countdown to the next shot

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f); // Call UpdateTarget every 0.5 seconds
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Rotate towards the target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        // Fire at the target
        if (fireCountdown <= 0.0f)
        {
            Shoot();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        // Find all enemies in range using Physics2D Overlapping Circle
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Find the nearest enemy
        foreach (Collider2D enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.gameObject;
            }
        }

        // Set the nearest enemy as the target if it's within range
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        // Spawn a projectile at the fire point
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            // Set the target of the projectile
            projectile.SetTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the range of the tower in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, range);
    }
}