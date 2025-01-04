using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Vector3 positionOrig;
    [SerializeField] int score;
    [SerializeField] float moveDuration = 1.5f;
    [SerializeField] float moveAmount = 1f;
    public static UnityAction<Vector3, int> OnGettingPoint;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        positionOrig = transform.position;
        StartCoroutine(Modulate());
    }

    IEnumerator Modulate()
    {
        float elaptedTime = 0f;
        float Offset = 0f;
        
        while (true)
        {
            elaptedTime += Time.deltaTime;
            Offset = Mathf.Sin(Time.time / moveDuration * (2 * Mathf.PI)) * moveAmount;
            transform.position = new Vector3(positionOrig.x, positionOrig.y + Offset, positionOrig.z);
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Collected");
            GameManager.ScoreAdd(score);
            OnGettingPoint?.Invoke(transform.position, score);
            Destroy(gameObject, 1f);
        }
    }

    void OnDisable()
    {
        StopCoroutine(Modulate());
    }
}
