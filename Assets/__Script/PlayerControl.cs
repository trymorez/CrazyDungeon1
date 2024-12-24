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
    [SerializeField] bool canJump = true;
    [SerializeField] bool isJumping = false;

    [Header("--- Side Check ---")]
    [SerializeField] Transform side;
    [SerializeField] Vector2 sideSize = new Vector2(2f, 2f);
    bool isOnWall;


    [Header("--- Jump ---")]
    [SerializeField] float jumpForce = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocityX = moveX * moveSpeed;
        CheckDirection();
        CheckBottom();
    }

    void CheckBottom()
    {
        if (Physics2D.OverlapBox(bottom.position, bottomSize, 0, bottomLayer))
        {
            canJump = true;
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
        if (isJumping)
        {
            return;
        }

        if (context.performed && canJump)
        {
            rb.linearVelocityY = jumpForce;
            canJump = false;
        }
        else if (context.canceled)
        {
            rb.linearVelocityY = rb.linearVelocityY / 2f;
            canJump = false;
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
