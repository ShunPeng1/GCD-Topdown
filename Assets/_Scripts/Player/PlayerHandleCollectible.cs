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
    [SerializeField] private TextMeshProUGUI _woodAmountText;
    [SerializeField] private TextMeshProUGUI _stoneAmountText;
    [SerializeField] private TextMeshProUGUI _ironAmountText;
    [SerializeField] private TextMeshProUGUI _bronzeAmountText;
    [SerializeField] private TextMeshProUGUI _silverAmountText;
    [SerializeField] private TextMeshProUGUI _goldAmountText;
    [SerializeField] private GameObject _invPanel;
    [SerializeField] private InputActionReference _openInvInputAction;
    private void Awake() {
        _openInvInputAction.action.performed += ctx => HandleInventory();
    }
    private void Start() {
        _woodAmountText.text = "x " + _woodAmount;
        _stoneAmountText.text = "x " + _stoneAmount;
        _ironAmountText.text = "x " + _ironAmount;
        _bronzeAmountText.text = "x " + _bronzeAmount;
        _silverAmountText.text = "x " + _silverAmount;
        _goldAmountText.text = "x " + _goldAmount;
    }
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
                _woodAmountText.text = "x " + _woodAmount;
                break;
            case CollectibleEnum.Stone:
                _stoneAmount += 1;
                _stoneAmountText.text = "x " + _stoneAmount;
                break;
            case CollectibleEnum.Iron:
                _ironAmount += 1;
                _ironAmountText.text = "x " + _ironAmount;
                break;
            case CollectibleEnum.Bronze:
                _bronzeAmount += 1;
                _bronzeAmountText.text = "x " + _bronzeAmount;
                break;
            case CollectibleEnum.Silver:
                _silverAmount += 1;
                _silverAmountText.text = "x " + _silverAmount;
                break;
            case CollectibleEnum.Gold:
                _goldAmount += 1;
                _goldAmountText.text = "x " + _goldAmount;
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

    private void HandleInventory() {
        _invPanel.SetActive(!_invPanel.activeInHierarchy);
    }
}
