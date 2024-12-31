using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{

    float rayLength = 1.0f;
    [SerializeField] LayerMask platformMask;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        int direction;

        while (isAlive)
        {
            direction = isFacingRight ? 1 : -1;
            rb.transform.Translate(Vector3.right * (direction * moveSpeed * Time.deltaTime));
            animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocityX));

            if (!IsGrounded() || IsBlocked())
            {
                
                isFacingRight = !isFacingRight;
                DirectionChange();
            }
            yield return null;
        }
        rb.linearVelocity = Vector3.zero;
        animator.SetFloat("velocityX", 0);
        animator.SetTrigger("isHit");
        gameObject.layer = deadEnemyLayer;
    }

    void DirectionChange()
    {
        Vector3 ls;
        ls = transform.localScale;
        ls.x *= -1;
        transform.localScale = ls;
    }

    bool IsBlocked()
    {
        Vector3 rayOrigin = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * 0.5f;
        return Physics2D.Raycast(rayOrigin, Vector2.right * (isFacingRight ? 1: -1), 0.3f, platformMask);

    }

    bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * 0.5f;
        return Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, platformMask);
    }

    void OnDrawGizmos()
    {
        DrawHeadGizmos();
        Vector3 rayOrigin = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayLength);
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.right * ((isFacingRight ? 1 : -1) * 0.5f));
    }

}
