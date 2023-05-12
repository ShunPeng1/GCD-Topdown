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
    private Transform _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Behaviour();
    }
    private void DetectPlayer() {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, _detectRange, _playerLayerMask);
        if (playerCol) {
            _target = playerCol.gameObject.transform;
        } else {
            _target = null;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
    private void Behaviour() {
        if (_target != null) {
            if (Vector2.Distance(transform.position, _target.position) <= _maxDisToOriginalPos && Vector2.Distance(transform.position, _target.position) > _attackRange) {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed*Time.deltaTime);
                
            }
            else if (Vector2.Distance(transform.position, _target.position) > _maxDisToOriginalPos) {
                transform.position = Vector2.MoveTowards(transform.position, _originalPos, _speed*Time.deltaTime);
            }
        } else {
            transform.position = Vector2.MoveTowards(transform.position, _originalPos, _speed*Time.deltaTime);
        }
    }
}
