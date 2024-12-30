using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static UnityAction<int> OnHealthChanged;
    public static UnityAction OnDead;

    [SerializeField] int healthMax = 3;
    int health;
    bool isInvincible;

    Rigidbody2D rb;
    Animator animator;
   

    void Start()
    {
        health = healthMax;
        OnHealthChanged?.Invoke(health);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Trap.OnPlayerHit += TakeDamage;
    }

    void OnDisable()
    {
        Trap.OnPlayerHit -= TakeDamage;
    }

    void TakeDamage(int damage, Vector3 hitPosition, bool instantDeath)
    {
        float force = 5f;
        
        if (isInvincible && !instantDeath)
        {
            return;
        }

        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        OnHealthChanged?.Invoke(health);
        animator.SetTrigger("isHit");
        rb.linearVelocity = Vector3.zero;
        rb.linearVelocityY = 1.0f * force;

        if (health <= 0 || instantDeath)
        {
            PlayerDie();
        }
        else
        {
            StartCoroutine(MakeInvincible(1));
        }
    }

    IEnumerator MakeInvincible(float timer)
    {
        isInvincible = true;

        Renderer renderer = GetComponentInChildren<Renderer>();

        float blinkDuration = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < timer)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(blinkDuration);
            elapsedTime += blinkDuration;
        }

        renderer.enabled = true;

        isInvincible = false;
    }

    void PlayerDie()
    {
        Debug.Log("dead!");
        OnDead.Invoke();
    }
}
