using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollectible : MonoBehaviour
{
    public Collectible CollectibleType;
    private void Start() {
        GetComponent<SpriteRenderer>().sprite = CollectibleType.CollectibleSprite;
    }
    public void Collect() {
        Destroy(gameObject);
    }
}
