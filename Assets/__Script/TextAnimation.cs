using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public AnimationCurve curve;
    [SerializeField] float duration = 1.5f;
    float elapsedTime;
    RectTransform rt;
    TextMeshProUGUI textMeshPro;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Step1());
        
    }

    IEnumerator Step1()
    {
        Vector3 scaleOriginal = rt.localScale;

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float scale = curve.Evaluate(elapsedTime / duration);
            rt.localScale = scaleOriginal * scale;
            yield return null;
        }
        StartCoroutine(Step2());

    }

    IEnumerator Step2()
    {
        float duration = 0.5f;
        float delay = 2f;
        Color startColor = textMeshPro.color;

        yield return new WaitForSeconds(delay);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startColor.a, 0, t / duration);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
    }
}
