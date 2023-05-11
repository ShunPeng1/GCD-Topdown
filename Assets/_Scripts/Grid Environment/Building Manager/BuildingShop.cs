using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShop : MonoBehaviour
{
 
    [Serializable]
    class BuildingShopItem
    {
        public BuildingShopItem BuildingItem;
        public BuildingBehaviour BuildingBehaviour;
        public CollectibleEnum Type1;
        public int Cost1;
        public CollectibleEnum Type2;
        public int Cost2;
    }

    [SerializeField] private List<BuildingShopItem> _buildingShopItems;
    public void Buy(int itemIndex)
    {
        BuildingShopItem buildingShopItem = _buildingShopItems[itemIndex];

        if (DataManager.Instance.GetCollectibleValue(buildingShopItem.Type1) >= buildingShopItem.Cost1 &&
            DataManager.Instance.GetCollectibleValue(buildingShopItem.Type2) >= buildingShopItem.Cost2)
        {
            DataManager.Instance.AddCollectibleValue(buildingShopItem.Type1, -buildingShopItem.Cost1);
            DataManager.Instance.AddCollectibleValue(buildingShopItem.Type2, -buildingShopItem.Cost2);
            BuildingManager.Instance.HoldBuilding();
        }
    }

}
