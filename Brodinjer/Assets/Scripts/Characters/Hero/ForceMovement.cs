using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovement : MonoBehaviour
{
    public CharacterController controller;
    public float JumpBackDistance, JumpTime;
    public string JumpBackString;
    public Animator anim;

    public float TurnTime;


    public void StopMove()
    {
        StopAllCoroutines();
        anim.SetTrigger("Idle");
        anim.SetFloat("Speed", 0);
    }

    public void JumpBack(Transform direction)
    {
        StartCoroutine(Dodge(direction.transform.forward, JumpBackDistance, JumpTime, JumpBackString));
    }
    private IEnumerator Dodge(Vector3 dodgeDirection, float amount, float dodgeTime, string animationString)
    {
        if(animationString != "")
        {
            anim.speed = 1;
            anim.gameObject.GetComponent<ResetTriggers>().ResetAllTriggers();
            anim.SetTrigger(animationString);
        }
        float currentTime = 0;
        while (currentTime < dodgeTime)
        {
            Vector3 _moveVec = dodgeDirection * amount;
            controller.Move(_moveVec * Time.deltaTime);
            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        anim.SetTrigger("Idle");
    }
    public void TurnAround(Transform direction)
    {
        StartCoroutine(Turn(direction, TurnTime));
    }

    private IEnumerator Turn(Transform direction, float turnTime)
    {
        float currentTime = 0;
        Quaternion origRotation = controller.transform.rotation;
        while(currentTime < turnTime)
        {
            currentTime += Time.deltaTime;
            controller.transform.rotation = Quaternion.Lerp(origRotation, direction.rotation, GeneralFunctions.ConvertRange(0, turnTime, 0, 1, currentTime));
            yield return new WaitForFixedUpdate();
        }
    }
}
