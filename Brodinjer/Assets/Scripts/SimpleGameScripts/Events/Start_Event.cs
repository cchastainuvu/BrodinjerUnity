using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Start_Event : MonoBehaviour
{
    public UnityEvent Event;
    public float waitTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(waitTime);
        Event.Invoke();
    }
}
