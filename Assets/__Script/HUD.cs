using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    void Start()
    {
        PlayerHealth.OnHealthChanged += UpdateHealth;
    }
    void UpdateHealth(int health)
    {
        Debug.Log(health);
    }
}
