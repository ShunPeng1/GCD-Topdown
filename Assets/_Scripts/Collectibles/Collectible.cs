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
public class Collectible : ScriptableObject {
    public Sprite CollectibleSprite;
    public CollectibleEnum CollectibleType;
    public int MinAmount;
    public int MaxAmount; 
    public Collectible(CollectibleEnum collectibleType, int maxAmount, int minAmount) {
        this.CollectibleType = collectibleType;
        this.MaxAmount = maxAmount;
        this.MinAmount = minAmount;
    }
}