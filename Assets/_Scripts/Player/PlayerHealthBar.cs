using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerSystem playerSystem;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    // Start is called before the first frame update
    
    private PlayerDataSO _playerData;
    
    void Start()
    {
        _playerData = playerSystem.PlayerData;
        totalHealthBar.fillAmount = _playerData.CurrentHealth / _playerData.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = _playerData.CurrentHealth / _playerData.MaxHealth;
    }
}
