using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static bool isGameOn = true;
    public static int levelCurrent = 0;
    public static int levelMax = 2;
    public static int healthMax = 3;
    public static int health = 3;
    public static int score = 0;
    public static UnityAction OnScoreChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ScoreAdd(int scoreToAdd)
    {
        score += scoreToAdd;
        OnScoreChanged?.Invoke();
    }

    public static void LevelLoadNext()
    {
        if (++levelCurrent > levelMax)
        {
            levelCurrent = 0;
        }
        SceneManager.LoadScene(levelCurrent);
    }

    public void LevelLoad(int level)
    {

    }    
}
