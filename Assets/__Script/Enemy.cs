using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    protected int currentHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isFacingRight;
    [SerializeField] protected GameObject head;
    [SerializeField] protected float headHeight;
    [SerializeField] protected float headWidth;
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected int enemyLayer = 11;
    [SerializeField] protected int enemyDeadLayer = 12;
    [SerializeField] protected int enemyTmpLayer = 13;

    public static UnityAction<int, Vector3, bool> OnPlayerHit;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector3 headSize;
    protected Vector3 headPosition;
    protected bool isAlive = true;

    protected virtual void Start()
    {
        isAlive = true;
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponentInChildren<Animator>();
        headSize = new Vector3(headWidth, headHeight, 0);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy got hit (on the head)
        headPosition = head.transform.position;
        if (Physics2D.OverlapBox(headPosition, headSize, 0, playerMask))
        {
            MakeUninteractable(2f);
            SoundFXManager.Play("StepOn");
            animator.SetBool("isHit", true);
            OnPlayerHit?.Invoke(0, transform.position, false);

            if (--currentHealth < 1)
            {
                EnemyDead();
            }
        }

        //Player got hit
        else if (collision.collider.CompareTag("Player"))
        {
            MakeUninteractable(2f);
            SoundFXManager.Play("Hit");
            Vector3 position = transform.position;
            OnPlayerHit?.Invoke(damage, position, false);
        }
    }

    void MakeUninteractable(float duration)
    {
        gameObject.layer = enemyTmpLayer;
        Invoke("Makeinteractable", duration);
    }
    void Makeinteractable()
    {
        gameObject.layer = enemyLayer;
    }

    void EnemyDead()
    {
        isAlive = false;
        Destroy(this.gameObject, 0.8f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        headPosition = head.transform.position;
        Gizmos.DrawWireCube(headPosition, headSize);
    }
}
