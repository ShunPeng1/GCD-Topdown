using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBahaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange;
    [Header("Enemy Follow")]
    [SerializeField] private float _detectRange;
    private Vector3 _originalPos;
    [SerializeField] private float _maxDisToOriginalPos;
    [SerializeField] private LayerMask _playerLayerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEnemyBehaviour(DetectPlayer()?.gameObject.transform);
    }
    private Collider2D DetectPlayer() {
        return Physics2D.OverlapCircle(transform.position, _detectRange, _playerLayerMask);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
    private void HandleEnemyBehaviour(Transform target) {
        if (Vector2.Distance(transform.position, _originalPos) > _maxDisToOriginalPos) {
            transform.position = Vector2.MoveTowards(transform.position, _originalPos, _speed*Time.deltaTime);
        }
        else {
            if (target != null) {
                if (Vector2.Distance(transform.position, target.position) > _attackRange) {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, _speed*Time.deltaTime);
                }
            }
        }
    }
}
