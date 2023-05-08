using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSystem))]
public class PlayerHealth : MonoBehaviour
{
    
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int flashesAmount;

    private PlayerSystem _playerSystem;
    private PlayerDataSO _playerData;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _playerSystem = GetComponent<PlayerSystem>();
        _playerData = _playerSystem.PlayerData;
        
        _playerData.MaxHealth = _playerData.InitHealth;
        _playerData.CurrentHealth = _playerData.InitHealth;
        _playerData.OnChangeCurrentHealth += ChangeHealthEvent;

        _renderer = GetComponent<SpriteRenderer>(); 
    }

    private void ChangeHealthEvent(float newHealth, float changeValue) {
        
        if(changeValue < 0) // Lose Health
        {
            if (newHealth > 0) {
                // anim.SetTrigger("Hurt");
                StartCoroutine(Invulnerability());
            }
            else {
                Debug.Log("Death");
            }
            
        }
        else // Add Health
        {
            
        }
    }

    public void ChangeHealth(float changeValue)
    {
        
    }
    
    private IEnumerator Invulnerability() {
        Physics2D.IgnoreLayerCollision(0, 5, true);
        for (int i = 0; i < flashesAmount; i++) {
            _renderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (2 * flashesAmount));
            _renderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (2 * flashesAmount));
        }
        Physics2D.IgnoreLayerCollision(0, 5, false);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerData.CurrentHealth -= 1;
        }
    }
}
