using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] List<Transform> navTransforms = new List<Transform>();
    [SerializeField] List<Vector3> navPoints = new List<Vector3>();
    [SerializeField] List<float> navDuration = new List<float>();
    [SerializeField] Vector3 navCurrent;
    [SerializeField] Vector3 navNext;
    [SerializeField] float delay;
    [SerializeField] int navCount;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //insert current position as the first element
        navTransforms.Insert(0, transform);
        navDuration.Insert(0, navDuration[navDuration.Count-1]);

        navPoints.Clear();
        for (int i = 0; i < navTransforms.Count; i++)
        {
            navPoints.Add(navTransforms[i].position);
        }
        navCount = navPoints.Count;
        StartCoroutine(Navigate());
    }

    void Update()
    {
        
    }



    IEnumerator Navigate()
    {
        float eplasedTime;
        int next;
        while (true)
        {
            for (int i = 0; i < navCount; i++)
            {
                eplasedTime = 0;
                next = i + 1;
                if (next == navCount)
                {
                    next = 0;
                }

                while (eplasedTime < navDuration[i])
                {
                    eplasedTime += Time.deltaTime;
                    float t = eplasedTime / navDuration[i];
                    float x = Mathf.Lerp(navPoints[i].x, navPoints[next].x, t);
                    float y = Mathf.Lerp(navPoints[i].y, navPoints[next].y, t);
                    transform.position = new Vector3(x, y, 0);

                    yield return null;
                }
            }
        }
    }
}
