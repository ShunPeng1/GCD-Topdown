using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHitPoint;
    [SerializeField] private LayerMask _playerLayerMask;
    private float currHitPoint;
    [SerializeField] private EnvironmentHealthBar healthBarBehavior;
    [SerializeField] private float detectionRange;
    // Start is called before the first frame update
    void Start()
    {
        currHitPoint = maxHitPoint;
        healthBarBehavior.SetHealth(currHitPoint, maxHitPoint);
    }

    private void TakeDamge(float _value) {
        currHitPoint = Mathf.Clamp(currHitPoint - _value, 0, maxHitPoint);
        healthBarBehavior.SetHealth(currHitPoint, maxHitPoint);
        if (currHitPoint <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown() {
        if (TargetDectection() != null) {
            TakeDamge(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private Collider2D TargetDectection() {
        return Physics2D.OverlapCircle(transform.position, detectionRange, _playerLayerMask);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
