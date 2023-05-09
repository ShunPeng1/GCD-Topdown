using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleCollectible : MonoBehaviour
{
    [Header("Player Magnet")]
    [SerializeField] private LayerMask _collectibleLayerMask;
    [SerializeField] private float _collectRange;
    [SerializeField] private float _magnetForce;
    private int _woodAmount = 0;
    [SerializeField] private int _stoneAmount = 0;
    private int _ironAmount = 0;
    private int _bronzeAmount = 0;
    private int _silverAmount = 0;
    private int _goodAmount = 0;
    private void FixedUpdate() {
        Collecting();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Collectible collectible = other.GetComponent<Collectible>();
        if (collectible != null) {
            collectible.Collect();
            Collected(collectible.CollectibleSoType);
        }
    }

    private void Collected(CollectibleSO collectibleSo) {
        switch (collectibleSo.CollectibleType) {
            case CollectibleEnum.Wood:
                _woodAmount += 1;
                break;
            case CollectibleEnum.Stone:
                _stoneAmount += 1;
                break;
            case CollectibleEnum.Iron:
                _ironAmount += 1;
                break;
            case CollectibleEnum.Bronze:
                _bronzeAmount += 1;
                break;
            case CollectibleEnum.Silver:
                _silverAmount += 1;
                break;
            case CollectibleEnum.Gold:
                _goodAmount += 1;
                break;
            default:
                Debug.Log("You've collected something that is out of this world.");
                break;
        }
    }

    private void Collecting() {
        Collider2D[] collectibleColliders = Physics2D.OverlapCircleAll(transform.position, _collectRange, _collectibleLayerMask);
        foreach (Collider2D collectibleCollider in collectibleColliders) {
            GameObject collectibleGameObject = collectibleCollider.gameObject;
            Vector2 direction = (transform.position - collectibleGameObject.transform.position).normalized;
            Debug.Log(direction);
            collectibleGameObject.GetComponent<Rigidbody2D>().AddForce(direction * _magnetForce);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _collectRange);
    }
}
