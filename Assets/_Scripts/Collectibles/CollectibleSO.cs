using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleEnum {
    Wood,
    Stone,
    Iron,
    Bronze,
    Silver,
    Gold
}

[CreateAssetMenu]
public class CollectibleSO : ScriptableObject {
    public Sprite CollectibleIcon;
    public CollectibleEnum CollectibleType;
    public Collectible CollectiblePrefab;
    
    public int MinAmount;
    public int MaxAmount; 
    public CollectibleSO(CollectibleEnum collectibleType, int maxAmount, int minAmount) {
        this.CollectibleType = collectibleType;
        this.MaxAmount = maxAmount;
        this.MinAmount = minAmount;
    }
}