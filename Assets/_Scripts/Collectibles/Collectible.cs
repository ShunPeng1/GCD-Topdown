using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleSO CollectibleSoType;
    private void Start() {
        GetComponent<SpriteRenderer>().sprite = CollectibleSoType.CollectibleSprite;
    }
    public void Collect() {
        Destroy(gameObject);
    }
}
