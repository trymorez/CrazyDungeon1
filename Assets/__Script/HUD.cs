using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class HUD : MonoBehaviour
{
    [SerializeField] Image heartPrefab;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject pointText;
    [SerializeField] RectTransform uiCanvas;
    List<Image> hearts = new List<Image>();
    RectTransform rt;
    int healthOld = 0;


    void Awake()
    {
        rt = GetComponentInParent<RectTransform>();
        PlayerHealth.OnHealthChanged += HealthUpdate;
        GameManager.OnScoreChanged += ScoreUpdate;
        Enemy.OnGettingPoint += PointShow;
    }

    void Start()
    {
        ScoreUpdate();
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= HealthUpdate;
        GameManager.OnScoreChanged -= ScoreUpdate;
        Enemy.OnGettingPoint -= PointShow;
    }

    void PointShow(Vector3 position, int point)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

        GameObject floatingText = Instantiate(pointText, uiCanvas);

        floatingText.GetComponent<RectTransform>().position = screenPosition;
        PointAnimation pointAnimation = floatingText.GetComponent<PointAnimation>();
        pointAnimation.point = point;
    }

    void ScoreUpdate()
    {
        scoreText.text = GameManager.score.ToString("D6");
    }

    void HealthUpdate(int health)
    {
        if (healthOld > health)
        {
            healthDelete(health);
        }
        if (healthOld < health)
        {
            healthAdd(health);
        }
        healthOld = health;
    }

    void healthAdd(int health)
    {
        for (int i = healthOld; i < health; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            hearts.Add(newHeart);
        }
    }    
    void healthDelete(int health)
    {
        for (int i = health; i < healthOld; i++)
        {
            if (i < hearts.Count)
            {
                hearts[i].GetComponent<Animator>().SetTrigger("isHit");
                Destroy(hearts[i].gameObject, 1.0f);
            }
        }

    }
}
