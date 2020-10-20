using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/Hands Movement")]
public class Hand_Animation : Animation_Base
{

    public string DirectionName;
    public string SpeedName;
    public string StartTriggerName, EndTriggerName;
    private bool animating;
    private Coroutine animateFunc;
    public float speedDif;
    
    
    public override void StartAnimation()
    {
        animating = true;
        anim.SetTrigger(StartTriggerName);
        animateFunc = caller.StartCoroutine(UpdateValues());
    }

    private IEnumerator UpdateValues()
    {
        while (animating)
        {
            anim.SetFloat(SpeedName, agent.velocity.magnitude);
            anim.SetFloat(DirectionName, GetDirection());
            anim.speed = agent.velocity.magnitude * speedDif;
            yield return new WaitForFixedUpdate();
        }
    }

    public override void StopAnimation()
    {
        animating = false;
        if(animateFunc!= null)
            caller.StopCoroutine(animateFunc);
        if (EndTriggerName != "")
        {
            anim.SetTrigger(EndTriggerName);
        }
    }

    public virtual float GetDirection()
    {
        float angle =  GeneralFunctions.GetDirection(agent.velocity + anim.transform.position, anim.transform.position);
        angle /= 360;
        angle += .5f;
        return angle;
    }
    
}
