using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Randomize_Event : MonoBehaviour
{
    public List<UnityEvent> EventList;

    public void RunRandomEvent()
    {
        EventList[Random.Range(0, EventList.Count)].Invoke();
        //EventList[1].Invoke();
    }
}
