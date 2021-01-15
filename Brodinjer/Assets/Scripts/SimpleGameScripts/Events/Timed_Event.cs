﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timed_Event : MonoBehaviour
{
    public bool OnAwake = false;
    public float WaitTime;
    public UnityEvent Event;
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
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        Event.Invoke();
    }

    public void Stop()
    {
        if(waitFunc!= null)
            StopCoroutine(waitFunc);
    }

    private void OnDisable()
    {
        Stop();
    }
}
