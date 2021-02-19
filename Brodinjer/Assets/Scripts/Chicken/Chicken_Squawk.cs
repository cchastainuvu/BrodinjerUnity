using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Squawk : MonoBehaviour
{
    public SoundController Squawk;
    private bool running;
    public float minWait, maxWait;
    public bool RunOnStart;

    private void Start()
    {
        running = false;
        if (RunOnStart)
            StartSquawk();
    }

    public void StartSquawk()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(SquawkRandom());
        }
    }

    private IEnumerator SquawkRandom()
    {
        yield return new WaitForSeconds(Random.Range(0, maxWait));
        while (running)
        {
            Squawk.Play();
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }

    public void StopSquawk()
    {
        running = false;
    }
}
