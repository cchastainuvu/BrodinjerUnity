using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Enter_Event : Trigger_Event_Base
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        StartCoroutine(CheckTrigger(other));
    }
}
