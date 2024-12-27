using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth.OnHealthChanged.AddListener(UpdateHealth);
    }

    void UpdateHealth(int health)
    {
        Debug.Log(health);
    }
}
