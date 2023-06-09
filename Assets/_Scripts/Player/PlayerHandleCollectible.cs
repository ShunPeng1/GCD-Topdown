using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerHandleCollectible : MonoBehaviour
{
    [Header("Player Magnet")]
    [SerializeField] private LayerMask _collectibleLayerMask;
    [SerializeField] private float _collectRange;
    [SerializeField] private float _magnetForce;
    [Header("Player Resources")]
    private int _woodAmount = 0;
    private int _stoneAmount = 0;
    private int _ironAmount = 0;
    private int _bronzeAmount = 0;
    private int _silverAmount = 0;
    private int _goldAmount = 0;
    
    [SerializeField] private GameObject _invPanel;
    [SerializeField] private InputActionReference _openInvInputAction;
    private void Awake() {
        _openInvInputAction.action.performed += ctx => HandleInventory();
    }
    
    private void FixedUpdate() {
        PullCollectible();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Collectible collectible = other.GetComponent<Collectible>();
        if (collectible != null) {
            collectible.Collect();
            DataManager.Instance.AddCollectibleValue(collectible.CollectibleSo.CollectibleType, 1);
        }
    }

    

    private void PullCollectible() {
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

    private void HandleInventory() {
        _invPanel.SetActive(!_invPanel.activeInHierarchy);
    }
}
