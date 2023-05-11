using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;

public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
{
    [Header("Environment UI")] 
    public GameObject EnvironmentOutline;


    [Header("Building")] 
    public BuildingBehaviour BombTower1;
    public BuildingBehaviour BombTower2;
    public BuildingBehaviour BombTower3;

}
