using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBahaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange;
    [Header("Enemy Follow")]
    [SerializeField] private float _detectRange;
    private Vector3 _originalPos;
    [SerializeField] private float _maxDisToOriginalPos;
    [SerializeField] private LayerMask _playerLayerMask;
    public Transform target {get; private set; }
    private Animator _animator;
    private bool _isFacingRight;
    [SerializeField] private AttackHitbox _attackHitbox;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.position;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Behaviour();
    }
    private void DetectPlayer() {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, _detectRange, _playerLayerMask);
        if (playerCol) {
            target = playerCol.gameObject.transform;
        } else {
            target = null;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
    private void Behaviour() {
        if (target != null) {
            if (Vector2.Distance(transform.position, target.position) <= _maxDisToOriginalPos && Vector2.Distance(transform.position, target.position) > _attackRange) {
                transform.position = MoveTowardsPlayer(target.position);
                // transform.position = Vector2.MoveTowards(transform.position, target.position, _speed*Time.deltaTime);
                // _animator.SetBool("isMoving", true);
            }
            else if (Vector2.Distance(transform.position, target.position) > _maxDisToOriginalPos) {
                transform.position = MoveTowardsPlayer(_originalPos);
                // transform.position = Vector2.MoveTowards(transform.position, _originalPos, _speed*Time.deltaTime);
                // _animator.SetBool("isMoving", true);
            } else if (Vector2.Distance(transform.position, target.position) <= _attackRange) {
                _animator.SetBool("isMoving", false);
                _animator.SetTrigger("Attack");
            }
        } else {
            if (transform.position != _originalPos) {
                transform.position = MoveTowardsPlayer(_originalPos);
                // transform.position = Vector2.MoveTowards(transform.position, _originalPos, _speed*Time.deltaTime);
                // _animator.SetBool("isMoving", true);
            } else {
                _animator.SetBool("isMoving", false);
            }
        }
    }
    private Vector2 MoveTowardsPlayer(Vector3 position) {
        float directionVectorX = transform.position.x - position.x;
        CheckDirectionToFace(directionVectorX > 0);
        _animator.SetBool("isMoving", true);
        return Vector2.MoveTowards(transform.position, position, _speed*Time.deltaTime);
    }
    private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		_isFacingRight = !_isFacingRight;
	}
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != _isFacingRight)
			Turn();
	}
    private void StopAttackHitbox() {
        _attackHitbox.StopAttackHitbox();
    }
    private void StartAttackHitbox() {
        _attackHitbox.StartAttackHitbox();
    }
}
