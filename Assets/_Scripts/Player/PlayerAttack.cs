using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference _pointerPosition;
    [SerializeField] private InputActionReference _playerAttack;
    [SerializeField] private float _attackCooldown;
    private float _attackTimer;
    private Vector2 _mousePos;
    #region ANIMATION CONTROL
	private Animator _animator;
	#endregion
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerAttack.action.performed += ctx => PlayerAttacking();
    }

    // Update is called once per frame
    void Update()
    {
        _attackTimer -= Time.deltaTime;
        _mousePos = GetPointerInput();
    }
    private Vector2 GetPointerInput() {
        Vector3 mousePos = _pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void PlayerAttacking() {
        if (_attackTimer <= 0) {
            Vector2 attackDir = new Vector2(_mousePos.x - transform.position.x, _mousePos.y - transform.position.y).normalized;
            _animator.SetFloat("xAttackDir", attackDir.x);
            _animator.SetFloat("yAttackDir", attackDir.y);
            _animator.SetTrigger("Attack");
            _attackTimer = _attackCooldown;
        }   
    }

    

    
}
