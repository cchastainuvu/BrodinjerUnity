using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Aim_Script : MonoBehaviour
{
    private bool aiming;
    public TransformData targetObj;
    public Transform CenterTarget;

    private Coroutine aimFunc;

    public void StartAim()
    {
        aiming = true;
        aimFunc = StartCoroutine(Aim());
    }
    
    private IEnumerator Aim()
    {
        while (aiming)
        {
            if (targetObj.transform != null)
            {
                transform.LookAt(targetObj.transform);
            }
            else
            {
                transform.LookAt(CenterTarget.position);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopAim()
    {
        aiming = false;
        if (aimFunc != null)
        {
            StopCoroutine(aimFunc);
        }
    }
}
