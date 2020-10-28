using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Random_Timed_Event : MonoBehaviour
{
    public float minTime, maxTime;
    public UnityEvent Event;
    public bool OnAwake;
    public bool Repeat;
    private bool repeating;

    private Coroutine waitFunc;
    
    private void Start()
    {
        if (OnAwake)
        {
            StartCoroutine(Wait());
        }
        
    }

    public void Call()
    {
        waitFunc = StartCoroutine(Wait());
        repeating = Repeat;
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Event.Invoke();
        while (repeating)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Event.Invoke();
        }
    }

    public void Stop()
    {
        if (waitFunc != null)
        {
            StopCoroutine(waitFunc);
        }

        repeating = false;
    }
    

}
