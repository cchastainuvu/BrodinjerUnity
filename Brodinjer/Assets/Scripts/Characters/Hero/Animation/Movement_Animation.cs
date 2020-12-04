using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
[CreateAssetMenu(menuName = "Character/Player/Animation/Movement")]
public class Movement_Animation : Animation_Base
{
    public string speedFloat;
    public string angleFloat;
    public string walkTrigger;
    private bool moving;
    public CharacterTranslate translate;
    private Coroutine updateFunc;
    

    private IEnumerator AnimateUpdate()
    {
        if (anim != null)
        {
            if(reset)
                reset.ResetAllTriggers();
            anim.SetTrigger(walkTrigger);
            while (moving)
            {
                anim.speed = 1;
                anim.SetFloat(speedFloat, GetSpeed());
                anim.SetFloat(angleFloat, GetDirection());
                yield return new WaitForFixedUpdate();
            }
        }
    }

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
        moving = true;
        updateFunc = caller.StartCoroutine(AnimateUpdate());
    }

    public override void StopAnimation()
    {
        moving = false;
        if (updateFunc != null)
        {
            caller.StopCoroutine(updateFunc);
        }    
    }

    public override Animation_Base GetClone()
    {
        Movement_Animation temp = CreateInstance<Movement_Animation>();
        temp.StartTriggerName = StartTriggerName;
        temp.StopTriggerName = StopTriggerName;
        temp.speedFloat = speedFloat;
        temp.angleFloat = angleFloat;
        temp.walkTrigger = walkTrigger;
        temp.translate = translate;
        return temp;
    }
}
