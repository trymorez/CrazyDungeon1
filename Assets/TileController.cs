using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScroller : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private Vector3 positionStart;

    void Start()
    {
        positionStart = transform.position;
    }

    void Update()
    {
        transform.position = positionStart + Vector3.left * Mathf.Repeat(Time.time * scrollSpeed, transform.localScale.x);
    }
}
