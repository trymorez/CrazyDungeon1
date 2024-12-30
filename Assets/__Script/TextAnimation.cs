using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public AnimationCurve curve;
    [SerializeField] float duration = 1.0f;
    float elapsedTime;
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(Modulate());
    }

    IEnumerator Modulate()
    {
        Vector3 scaleOriginal = rt.localScale;

        while (true)
        {
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float scale = curve.Evaluate(elapsedTime / duration);
                rt.localScale = scaleOriginal * scale;
                yield return null;
            }
         

        }
        
    }

}
