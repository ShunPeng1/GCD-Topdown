using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLoot : MonoBehaviour
{
    [SerializeField] private GameObject _collectiblePrefab;
    [SerializeField] private Collectible _collectible;
    [SerializeField] private float _dropForce;
    // Start is called before the first frame update
    public void InstantiateCollectibles(Vector3 spawnPosition) {
        int rand = Random.Range(_collectible.MinAmount, _collectible.MaxAmount);
        for (int i = 0; i < rand; i++) {
            GameObject collectibleGameObject = Instantiate(_collectiblePrefab, spawnPosition, Quaternion.identity);
            collectibleGameObject.GetComponent<HandleCollectible>().CollectibleType = _collectible;

            Vector2 spawnDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            collectibleGameObject.GetComponent<Rigidbody2D>().AddForce(spawnDirection * _dropForce);
        }
    }
}
