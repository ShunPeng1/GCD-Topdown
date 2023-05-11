using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectible : MonoBehaviour
{
    public CollectibleSO CollectibleSo;
    
    private void Start() {
        GetComponent<SpriteRenderer>().sprite = CollectibleSo.CollectibleIcon;
    }
    public void Collect() {
        Destroy(gameObject);
    }
}
