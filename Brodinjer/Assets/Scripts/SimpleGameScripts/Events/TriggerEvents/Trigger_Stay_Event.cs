using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Stay_Event : Trigger_Event_Base
{
    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(CheckTrigger(other));
    }
}
