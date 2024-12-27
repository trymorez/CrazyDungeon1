using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int healthMax = 5;
    Animator animator;
    int health;
    public static UnityAction<int> OnHealthChanged;
    public HUD hud;

    void Start()
    {
        health = healthMax;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Trap.OnPlayerHit += TakeDamage;
    }

    void TakeDamage(int damage, Vector3 hitPosition)
    {
        float force = 20f;
        health -= damage;
        OnHealthChanged?.Invoke(health);
        animator.SetTrigger("isHit");
        rb.linearVelocity = (rb.transform.position - hitPosition).normalized * force;

        if (health < 1)
        {
            PlayerDie();
        }
    }
        
    void PlayerDie()
    {
        Debug.Log("dead!");
    }
}
