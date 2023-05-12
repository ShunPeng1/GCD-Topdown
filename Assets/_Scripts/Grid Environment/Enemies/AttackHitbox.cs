using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }
    public void StopAttackHitbox() {
        _collider.enabled = false;
    }
    public void StartAttackHitbox() {
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerBehaviour>().PlayerData.CurrentHealth -= 1;
        }
    }

}
