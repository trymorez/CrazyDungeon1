using UnityEngine;

public class Door : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.LevelLoadNext();
        }
    }
}
