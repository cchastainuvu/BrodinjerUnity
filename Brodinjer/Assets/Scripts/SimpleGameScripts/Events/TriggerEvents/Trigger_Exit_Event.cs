using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Exit_Event : Trigger_Event_Base
{
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CheckTrigger(other));
    }
}
