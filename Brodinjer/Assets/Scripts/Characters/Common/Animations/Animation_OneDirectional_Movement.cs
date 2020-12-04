using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/One Directional Movement")]
public class Animation_OneDirectional_Movement : Animation_Base
{

    public string SpeedName;
    private bool animating;
    private Coroutine animateFunc;
    public float speedDif;
    private ResetTriggers resettrigger;
    
    public override void StartAnimation()
    {
        animating = true;
        anim.SetTrigger(StartTriggerName);
        animateFunc = caller.StartCoroutine(UpdateValues());
    }

    private IEnumerator UpdateValues()
    {
        resettrigger = anim.gameObject.GetComponent<ResetTriggers>();
        if(resettrigger != null)
            resettrigger.ResetAllTriggers();
        anim.SetTrigger(StartTriggerName);
        while (animating)
        {
            anim.SetFloat(SpeedName, agent.velocity.magnitude);
            anim.speed = agent.velocity.magnitude * speedDif;
            if (anim.speed < 1)
            {
                anim.speed = 1;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public override void StopAnimation()
    {
        anim.ResetTrigger(StartTriggerName);
        animating = false;
        if(animateFunc!= null)
            caller.StopCoroutine(animateFunc);
        if (StopTriggerName != "")
        {
            anim.SetTrigger(StopTriggerName);
        }
    }

    public override Animation_Base GetClone()
    {
        Animation_OneDirectional_Movement temp = CreateInstance<Animation_OneDirectional_Movement>();
        temp.StartTriggerName = this.StartTriggerName;
        temp.StopTriggerName = this.StopTriggerName;
        temp.SpeedName = SpeedName;
        temp.speedDif = speedDif;
        return temp;
    }
}
