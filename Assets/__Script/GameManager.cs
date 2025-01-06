using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
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
    public static bool levelProgressTrigger = false;
    public static bool levelLoadedTrigger = false;
    public Image screenShadow;
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

    void Start()
    {
        screenShadow.gameObject.SetActive(false);
        Debug.Log("OnSceneLoaded");
    }

    void Update()
    {
        //need to improve
        if (levelProgressTrigger)
        {
            StartCoroutine(DelayAndLevelLoadNext(1f));
            levelProgressTrigger = false;
        }
        //need to improve
        if (levelLoadedTrigger)
        {
            screenShadow.gameObject.SetActive(false);
            levelLoadedTrigger = false;
        }
    }

    IEnumerator DelayAndLevelLoadNext(float delay)
    {
        float a;

        screenShadow.gameObject.SetActive(true);
        for (float t = 0f; t < delay; t += Time.deltaTime)
        {
            a = Mathf.Lerp(0f, 1f, t / delay);
            screenShadowSetAlpha(a);
            yield return null;
        }
        LevelLoadNext();
    }

    void screenShadowSetAlpha(float alpha)
    {
        Color color = screenShadow.color;

        color.a = alpha;
        screenShadow.color = color;
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
        levelLoadedTrigger = true;
    }

    public void LevelLoad(int level)
    {

    }    
}
