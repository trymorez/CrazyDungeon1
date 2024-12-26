using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] enum ePushDirection { up, down, left, right }
    [SerializeField] float pushLength = 3.5f;
    [SerializeField] float pushSpeed = 3f;
    [SerializeField] float pullSpeed = 2f;
    [SerializeField] float cycleDelay = 0.5f;
    [SerializeField] GameObject[] chains;
    bool initialized = true;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        while (pushProgress <= pushLength)
        {
            pushProgress += pushSpeed * Time.deltaTime;
            rb.transform.position = origPosition + new Vector3(0, pushProgress, 0);
            for (int i = 0; i < chains.Length-1; i++)
            {
                if (pushProgress >= 0.3 + i * 0.386)
                {
                    chains[i+1].SetActive(true);
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(cycleDelay);
        while (pushProgress >= 0)
        {
            pushProgress -= pullSpeed * Time.deltaTime;
            rb.transform.position = origPosition + new Vector3(0, pushProgress, 0);
            for (int i = 0; i < chains.Length-1; i++)
            {
                if (pushProgress <= i * 0.386)
                {
                    chains[i+1].SetActive(false);
                }
            }
            yield return null;
        }
        initialized = true;
    }
}
