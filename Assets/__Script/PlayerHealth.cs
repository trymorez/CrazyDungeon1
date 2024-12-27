using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthMax = 5;
    int health;
    public UnityEvent<int> OnHealthChanged;

    void Start()
    {
        health = healthMax;
        Trap.OnPlayerHit += TakeDamage;
    }

    void TakeDamage(int damage, Vector3 positionHit)
    {
        health -= damage;

        OnHealthChanged?.Invoke(health);

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
