using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{

    float rayLength = 1.5f;
    [SerializeField] LayerMask platformMask;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        int direction;

        while (true)
        {

            direction = isFacingRight ? 1 : -1;
            // rb.linearVelocityX = direction * moveSpeed * Time.deltaTime;
            rb.AddForceX(direction * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

            if (!IsGrounded())
            {
                rb.linearVelocityX = 0;
                isFacingRight = !isFacingRight;
                DirectionChange();
            }
            yield return null;
        }
    }

    void DirectionChange()
    {
        Vector3 ls;
        ls = transform.localScale;
        ls.x *= -1;
        transform.localScale = ls;
    }

    bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * 0.5f;
        return Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, platformMask).collider != null;
    }

    private void OnDrawGizmos()
    {
        Vector3 rayOrigin = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayLength);
    }
}
