using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHorizontal : MonoBehaviour
{
    private Collider2D _attackCollider;
    private Vector2 _rightOffset;
    // Start is called before the first frame update
    void Start()
    {
        _attackCollider = GetComponent<Collider2D>();
        _rightOffset = _attackCollider.offset;
    }
    public void AttackRight() {
        _attackCollider.offset = _rightOffset;
        _attackCollider.enabled = true;
    }
    public void AttackLeft() {
        _attackCollider.offset = new Vector3(_rightOffset.x * -1, _rightOffset.y);
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
