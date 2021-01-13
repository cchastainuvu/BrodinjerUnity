using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Animation/Attack/Multi Attack Animation")]

public class Animation_Multi_Attack : Animation_Base
{
    private ResetTriggers resettrigger;
    public int NumAnimations;
    public List<float> percentages;
    public string typeString;

    private float randomNum;
    
    public override void StartAnimation()
    {
        if (anim != null)
        {
            anim.speed = 1;
            resettrigger = anim.gameObject.GetComponent<ResetTriggers>();
            if (resettrigger != null)
                resettrigger.ResetAllTriggers();
            anim.SetFloat(typeString, GetNum());
            anim.SetTrigger(StartTriggerName);
        }
    }

    public override void StopAnimation()
    {
        anim.ResetTrigger(StartTriggerName);
        if (StopTriggerName != "")
        {
            anim.SetTrigger(StopTriggerName);
        }
        
    }

    public override Animation_Base GetClone()
    {
        Animation_Multi_Attack temp = CreateInstance<Animation_Multi_Attack>();
        temp.StartTriggerName = this.StartTriggerName;
        temp.StopTriggerName = this.StopTriggerName;
        temp.NumAnimations = this.NumAnimations;
        temp.percentages = percentages;
        temp.typeString = typeString;
        return temp;
    }

    private float GetNum()
    {
        randomNum = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < percentages.Count-1; i++)
        {
            if (randomNum >= percentages[i] && randomNum <= percentages[i + 1])
            {
                return (i + 1.0f) / (percentages.Count);
            }
        }

        return 1;
    }
}
