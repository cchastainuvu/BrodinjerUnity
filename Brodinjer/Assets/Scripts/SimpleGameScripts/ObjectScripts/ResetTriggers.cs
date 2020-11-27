using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggers : MonoBehaviour
{
    public Animator anim;
    public List<string> triggerNames;

    public void ResetAllTriggers()
    {
        foreach (var triggername in triggerNames)
        {
            anim.ResetTrigger(triggername);
        }
    }
}
