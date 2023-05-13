using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DirectionEnum {
    Left, Right, Up, Down
};
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
    [SerializeField] private PlayerAttackHorizontal _playerAttackHorizontal;
    [SerializeField] private PlayerAttackVertical _playerAttackVertical;
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
    private DirectionEnum GetDirection(float x, float y)
    {
        // Calculate the absolute values of x and y
        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);

        // Check the dominant direction
        if (absX > absY)
        {
            // Horizontal direction (left or right)
            if (x > 0)
                return DirectionEnum.Right;
            else
                return DirectionEnum.Left;
        }
        else
        {
            // Vertical direction (up or down)
            if (y > 0)
                return DirectionEnum.Up;
            else
                return DirectionEnum.Down;
        }
    }
    
    private void PlayerAttacking() {
        if (_attackTimer <= 0) {
            Vector2 attackDir = new Vector2(_mousePos.x - transform.position.x, _mousePos.y - transform.position.y).normalized;
            
            _animator.SetFloat("xAttackDir", attackDir.x);
            _animator.SetFloat("yAttackDir", attackDir.y);
            _animator.SetTrigger("Attack");

            switch(GetDirection(attackDir.x, attackDir.y)) {
                case DirectionEnum.Right:
                    _playerAttackHorizontal.AttackRight();
                    break;
                case DirectionEnum.Left:
                    _playerAttackHorizontal.AttackLeft();
                    break;
                case DirectionEnum.Up:
                    _playerAttackVertical.AttakUp();
                    break;
                case DirectionEnum.Down:
                    _playerAttackVertical.AttackDown();
                    break;
            }

            _attackTimer = _attackCooldown;
        }
    }
    private void StopAttack() {
        _playerAttackHorizontal.StopAttack();
        _playerAttackVertical.StopAttack();
    }

    

    
}
