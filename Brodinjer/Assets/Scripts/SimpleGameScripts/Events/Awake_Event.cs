using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Awake_Event : MonoBehaviour
{
    public UnityEvent Event;

    private void Awake()
    {
        Event.Invoke();
    }
}
