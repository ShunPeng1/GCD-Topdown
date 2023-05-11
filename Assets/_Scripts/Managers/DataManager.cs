using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    private Dictionary<CollectibleEnum, int> _collectibleValues;

    private void Start()
    {
        foreach (CollectibleEnum collectibleEnum in Enum.GetValues(typeof(CollectibleEnum)))
        {
            _collectibleValues[collectibleEnum] = 0;
        }
        
    }

    public int GetCollectibleValue(CollectibleEnum collectibleEnum)
    {
        return _collectibleValues[collectibleEnum];
    }

    public void AddCollectibleValue(CollectibleEnum collectibleEnum, int value)
    {
        _collectibleValues[collectibleEnum] += value;
    }
}
