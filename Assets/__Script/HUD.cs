using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image heartPrefab;
    List<Image> hearts = new List<Image>();
    int healthPrev = 0;

    void Awake()
    {
        PlayerHealth.OnHealthChanged += healthUpdate;
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= healthUpdate;
    }
    

    void healthUpdate(int health)
    {
        if (healthPrev > health)
        {
            healthDelete(health);
        }
        if (healthPrev < health)
        {
            healthAdd(health);
        }
        healthPrev = health;
    }

    void healthAdd(int health)
    {
        for (int i = healthPrev; i < health; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            hearts.Add(newHeart);
        }
    }    
    void healthDelete(int health)
    {
        for (int i = health; i < healthPrev; i++)
        {
            if (i < hearts.Count)
            {
                hearts[i].GetComponent<Animator>().SetTrigger("isHit");
                Destroy(hearts[i].gameObject, 1.0f);
            }
        }

    }
}
