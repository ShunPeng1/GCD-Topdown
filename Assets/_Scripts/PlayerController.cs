using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float collisionOffset = 0.01f;
    Rigidbody2D playerRb;
    Vector2 moveInput;
    public ContactFilter2D contactFilter;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if (moveInput != Vector2.zero) {
            bool canMove = TryMove(moveInput);

            if (!canMove) {
                canMove = TryMove(new Vector2(moveInput.x, 0));

                if (!canMove) {
                    canMove = TryMove(new Vector2(0, moveInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        int count = playerRb.Cast(direction, contactFilter, castCollision, speed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0) {
            playerRb.MovePosition(playerRb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    void OnMove(InputValue moveValue) {
        moveInput = moveValue.Get<Vector2>();
    }
}
