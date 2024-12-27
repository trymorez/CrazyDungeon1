using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    public int damage = 1;
    public static UnityAction<int, Vector3> OnPlayerHit;
    Rigidbody2D rb;
    Vector3 position;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            position = rb.transform.position;
            OnPlayerHit?.Invoke(damage, position);
        }
    }
}
