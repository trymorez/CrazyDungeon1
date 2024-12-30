using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected bool isFacingRight;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Dead()
    {
        Debug.Log("NPC Dead");
    }
}
