using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] enum ePushDirection { up, down, left, right }
    [SerializeField] ePushDirection pushDirection = ePushDirection.up;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float pushLength = 3.5f;
    [SerializeField] float pushSpeed = 3f;
    [SerializeField] float pullSpeed = 2f;
    [SerializeField] float cycleDelay = 0.5f;
    [SerializeField] GameObject[] chains;
    bool initialized = true;
    bool isBlock = false;
    float direction = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (pushDirection == ePushDirection.down || pushDirection == ePushDirection.left)
        {
            direction *= -1f;            
        }
    }

    void Update()
    {
        
        if (initialized)
        {
            StartCoroutine(SpikePush());
        }
    }

    IEnumerator SpikePush()
    {
        Vector3 origPosition = rb.transform.position;
        float pushProgress = 0f;
        initialized = false;

        yield return new WaitForSeconds(cycleDelay);

        while (pushProgress <= pushLength && !isBlock)
        {
            pushProgress += pushSpeed * Time.deltaTime;
            if (pushDirection == ePushDirection.right || pushDirection == ePushDirection.left)
            {
                rb.transform.position = origPosition + new Vector3(pushProgress * direction, 0, 0);
            }
            else
            {
                rb.transform.position = origPosition + new Vector3(0, pushProgress * direction, 0);
            }
            yield return null;
        }
        
        while (pushProgress >= 0)
        {
            pushProgress -= pullSpeed * Time.deltaTime;
            if (pushDirection == ePushDirection.right || pushDirection == ePushDirection.left)
            {
                rb.transform.position = origPosition + new Vector3(pushProgress * direction, 0, 0);
            }
            else
            {
                rb.transform.position = origPosition + new Vector3(0, pushProgress * direction, 0);
            }
            yield return null;
        }
        rb.transform.position = origPosition;
        pushProgress = 0f;
        initialized = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            isBlock = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            isBlock = false;
        }
    }
}
