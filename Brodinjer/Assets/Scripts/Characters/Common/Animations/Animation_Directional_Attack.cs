using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/Attack/Directional Attack")]

public class Animation_Directional_Attack : Animation_Base
{

    public string DirectionName;
    private float angle;
    private ResetTriggers resettrigger;
    
    public override void StartAnimation()
    {
        anim.speed = 1;
        resettrigger = anim.gameObject.GetComponent<ResetTriggers>();
        if(resettrigger != null)
            resettrigger.ResetAllTriggers();
        anim.SetFloat(DirectionName, DirectionalInput());
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

    public float DirectionalInput()
    {
        angle = GeneralFunctions.GetDirection(player.transform.position, agent.transform.position);
        angle /= 360;
        angle += .5f;
        return angle;
    }
    
    public override Animation_Base GetClone()
    {
        Animation_Directional_Attack temp = CreateInstance<Animation_Directional_Attack>();
        temp.StartTriggerName = this.StartTriggerName;
        temp.StopTriggerName = this.StopTriggerName;
        temp.DirectionName = this.DirectionName;
        return temp;
    }
}
