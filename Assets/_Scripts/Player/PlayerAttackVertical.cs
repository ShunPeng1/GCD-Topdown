using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackVertical : MonoBehaviour
{
    private Collider2D _attackCollider;
    private Vector2 _upOffset;
    // Start is called before the first frame update
    void Start()
    {
        _attackCollider = GetComponent<Collider2D>();
        _upOffset = _attackCollider.offset;
    }
    public void AttakUp() {
        _attackCollider.offset = _upOffset;
        _attackCollider.enabled = true;
    }
    public void AttackDown() {
        _attackCollider.offset = new Vector3(_upOffset.x, _upOffset.y * -1);
        _attackCollider.enabled = true;
    }
    public void StopAttack() {
        _attackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnvironmentHealth>().TakeDamage(1);
        }
    }
}
