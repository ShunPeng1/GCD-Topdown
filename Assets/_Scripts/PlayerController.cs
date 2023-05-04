using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public float CollisionOffset = 0.01f;
    Rigidbody2D _playerRb;
    Vector2 _moveInput;
    public ContactFilter2D ContactFilter;
    List<RaycastHit2D> _castCollision = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if (_moveInput != Vector2.zero) {
            bool canMove = TryMove(_moveInput);

            if (!canMove) {
                canMove = TryMove(new Vector2(_moveInput.x, 0));

                if (!canMove) {
                    canMove = TryMove(new Vector2(0, _moveInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        int count = _playerRb.Cast(direction, ContactFilter, _castCollision, Speed * Time.fixedDeltaTime + CollisionOffset);
        if (count == 0) {
            _playerRb.MovePosition(_playerRb.position + direction * Speed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    void OnMove(InputValue moveValue) {
        _moveInput = moveValue.Get<Vector2>();
    }
}
