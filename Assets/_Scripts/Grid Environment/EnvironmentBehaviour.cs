using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehaviour : GridXYGameObject
{
    [SerializeField] private float _maxHitPoint;
    [SerializeField] private LayerMask _playerLayerMask;
    private float _currHitPoint;
    [SerializeField] private EnvironmentHealthBar _healthBarBehavior;
    [SerializeField] private float _detectionRange;
    private Animator _animator;
    [SerializeField] private GameObject _outline;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _currHitPoint = _maxHitPoint;
        _healthBarBehavior.SetHealth(_currHitPoint, _maxHitPoint);
        _animator = GetComponent<Animator>();
    }

    private void TakeDamage(float _value) {
        _currHitPoint = Mathf.Clamp(_currHitPoint - _value, 0, _maxHitPoint);
        _healthBarBehavior.SetHealth(_currHitPoint, _maxHitPoint);
        if (_currHitPoint <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter() {
        _outline.SetActive(true);

    }

    private void OnMouseExit() {
        _outline.SetActive(false);
    }

    private void OnMouseDown() {
        if (TargetDetection() != null) {
            TakeDamage(1);
        }
    }
    private Collider2D TargetDetection() {
        return Physics2D.OverlapCircle(transform.position, _detectionRange, _playerLayerMask);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }

    private void OnDestroy() {
        GetComponent<EnvironmentLoot>().InstantiateCollectibles(transform.position);
    }
}
