using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOver;

    void Start()
    {
        PlayerHealth.OnDead += GameOver;
    }
    void OnDisable()
    {
        PlayerHealth.OnDead -= GameOver;
    }

    void GameOver()
    {
        GameController.isGameOn = false;
        gameOver.SetActive(true);
    }
}
