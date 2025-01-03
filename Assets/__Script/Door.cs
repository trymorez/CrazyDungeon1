using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
            StartCoroutine(DelayAndLevelLoadNext(1.0f));
        }
    }

    IEnumerator DelayAndLevelLoadNext(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.LevelLoadNext();
    }
}
