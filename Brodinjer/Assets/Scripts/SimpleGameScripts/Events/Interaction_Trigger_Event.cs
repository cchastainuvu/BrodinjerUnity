using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_Trigger_Event : Trigger_Event_Base
{
    private bool inrange, checking;
    public bool CheckOnStart;
    public UnityEvent inRangeEvent, outOfRangeEvent;
    private Coroutine checkFunc;
    public string InteractButton = "Interact";

    private void Start()
    {
        if (CheckOnStart)
            StartCheck();
    }

    public void StartCheck()
    {
        if (!checking)
        {
            checking = true;
            checkFunc = StartCoroutine(CheckInteract());
        }
    }

    public void StopCheck()
    {
        if (checking)
        {
            checking = false;
            if (checkFunc != null)
            {
                StopCoroutine(checkFunc);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!inrange)
        {
            switch (checksFor)
            {
                case Check.Layer:
                    if (other.gameObject.layer == ToLayer(layer.value))
                    {
                        inrange = true;
                        inRangeEvent.Invoke();
                    }

                    break;
                case Check.Name:
                    if (other.gameObject.name.Contains(objName))
                    {
                        inrange = true;
                        inRangeEvent.Invoke();
                    }

                    break;
                case Check.Tag:
                    if (other.gameObject.CompareTag(tagName))
                    {
                        inrange = true;
                        inRangeEvent.Invoke();
                    }

                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (inrange)
        {
            switch (checksFor)
            {
                case Check.Layer:
                    if (other.gameObject.layer == ToLayer(layer.value))
                    {
                        inrange = false;
                        outOfRangeEvent.Invoke();
                    }

                    break;
                case Check.Name:
                    if (other.gameObject.name.Contains(objName))
                    {
                        inrange = false;
                        outOfRangeEvent.Invoke();
                    }

                    break;
                case Check.Tag:
                    if (other.gameObject.CompareTag(tagName))
                    {
                        inrange = false;
                        outOfRangeEvent.Invoke();
                    }

                    break;
            }
        }
    }

    private IEnumerator CheckInteract()
    {
        Debug.Log("Start Check");
        while (checking)
        {
            if (Input.GetButtonDown(InteractButton) && inrange)
            {
                RunEvent();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
