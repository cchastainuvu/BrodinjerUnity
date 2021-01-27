using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownTrigger : Trigger_Event_Base
{
    public float Timer;
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        
        StartCoroutine(CheckTrigger(other));
    }

    public override void RunEvent()
    {
        if (!triggered)
        {
            triggered = true;
            StartCoroutine(ResetTrigger());
            base.RunEvent();
        }
    }

    private IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(Timer);
        triggered = false;
    }
}
