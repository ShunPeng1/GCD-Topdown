using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool isDead;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int flashesAmount;
    private SpriteRenderer _renderer;
    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamge(float _dmgValue) {
        currentHealth = Mathf.Clamp(currentHealth - _dmgValue, 0, startingHealth);
        if (currentHealth > 0) {
            // anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
        }
        else {
            if (!isDead) {
                // anim.SetTrigger("Dead");
                // GetComponent<PlayerController>().enabled = false;
                isDead = true;
            }
            
        }
    }
    public void AddHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
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
    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.Space)) {
    //         TakeDamge(1);
    //     }
    // }
}
