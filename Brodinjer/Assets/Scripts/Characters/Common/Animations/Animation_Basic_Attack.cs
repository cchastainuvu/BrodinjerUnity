using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/Attack/Trigger Attack")]

public class Animation_Basic_Attack : Animation_Base
{
    private ResetTriggers resettrigger;
    
    public override void StartAnimation()
    {
        anim.speed = 1;
        resettrigger = anim.gameObject.GetComponent<ResetTriggers>();
        if(resettrigger != null)
            resettrigger.ResetAllTriggers();
        anim.SetTrigger(StartTriggerName);
    }

    public override void StopAnimation()
    {
        anim.ResetTrigger(StartTriggerName);
        if (StopTriggerName != "")
        {
            anim.SetTrigger(StopTriggerName);
        }
        
    }
}
