using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timed_Event : MonoBehaviour
{
    public bool OnAwake = false;
    public float WaitTime;
    public UnityEvent Event;

    private void Start()
    {
        if (OnAwake)
        {
            StartCoroutine(Wait());
        }
        
    }

    public void Call()
    {
        StartCoroutine(Wait());
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        Event.Invoke();
    }
}
