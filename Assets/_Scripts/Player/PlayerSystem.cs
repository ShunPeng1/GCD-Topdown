using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateEnum
{
    Pausing,
    Crafting,
    Playing,
    Dying
}

public class PlayerSystem : MonoBehaviour
{
    public PlayerDataSO PlayerData;
    public PlayerStateEnum PlayerState;
    private void Start()
    {
        
    }
}
