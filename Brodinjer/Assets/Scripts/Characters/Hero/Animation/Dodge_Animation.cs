using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/Dodge")]
public class Dodge_Animation : Animation_Base
{
    public string directionFloat;
    public string dodgeTrigger;
    public CharacterTranslate translate;

    private float GetDirection()
    {
        return translate.getMoveAngle();
    }

    private float GetSpeed()
    {
        return translate.getSpeed();
    }

    public override void StartAnimation()
    {
        if (anim != null)
        {
            if (reset)
                reset.ResetAllTriggers();
            anim.SetFloat(directionFloat, GetDirection());
            anim.SetTrigger(dodgeTrigger);

        }
    }

    public override void StopAnimation()
    {

    }

    public override Animation_Base GetClone()
    {
        Dodge_Animation temp = CreateInstance<Dodge_Animation>();
        temp.directionFloat = directionFloat;
        temp.dodgeTrigger = dodgeTrigger;
        temp.StartTriggerName = StartTriggerName;
        temp.StopTriggerName = StopTriggerName;
        return temp;
    }
}
