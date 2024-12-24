using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [Header("--- Movement ---")]
    float moveX;
    [SerializeField] float moveSpeed = 8f;
    bool isFacingRight = true;

    [Header("--- Bottom Check ---")]
    [SerializeField] Transform bottom;
    [SerializeField] Vector2 bottomSize = new Vector2(2f, 2f);
    [SerializeField] LayerMask bottomLayer;
    [SerializeField] bool isJumping = false;
    [SerializeField] float fallGravity = 2.0f;
    float origGravity;

    [Header("--- Side Check ---")]
    [SerializeField] Transform side;
    [SerializeField] Vector2 sideSize = new Vector2(2f, 2f);
    [SerializeField] LayerMask sideLayer;
    [SerializeField] float sideSlideSpeed = -2.0f;
    bool isOnWall;


    [Header("--- Jump ---")]
    [SerializeField] float jumpForce = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        rb.linearVelocityX = moveX * moveSpeed;

        CheckDirection();
        CheckBottom();
        CheckSide();
        accelFalling();
    }

    void accelFalling()
    {
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = origGravity * fallGravity;
        }
    }

    bool IsTouchingSide()
    {
        return Physics2D.OverlapBox(side.position, sideSize, 0, sideLayer);
    }    

    void CheckSide()
    {
        if (IsTouchingSide() && isJumping)
        {
            rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, sideSlideSpeed);
        }
    }

    void CheckBottom()
    {
        if (rb.linearVelocityY > 0)
        {
            return;
        }
        if (Physics2D.OverlapBox(bottom.position, bottomSize, 0, bottomLayer))
        {
            isJumping = false;
        }
    }

    void ChangeDirection()
    {
        isFacingRight = !isFacingRight;
        Vector3 ls = rb.transform.localScale;
        ls.x *= -1f;
        rb.transform.localScale = ls;
    }

    void CheckDirection()
    {
        if (isFacingRight && moveX < 0 || !isFacingRight && moveX > 0)
        {
            ChangeDirection();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            rb.gravityScale = origGravity;
            isJumping = true;
            rb.linearVelocityY = jumpForce;
        }
        else if (context.canceled && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY *= 0.5f;
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bottom.transform.position, bottomSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(side.transform.position, sideSize);
    }

}
