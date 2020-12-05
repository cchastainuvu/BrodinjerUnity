using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Hand_Init_Scene : MonoBehaviour
{
    public UnityEvent endEvent;
    public Animator HandAnim, DoorAnim;
    public NavMeshAgent HandAgent;

    public GameObject SceneCam, PlayerCam;

    public float InitWait, HandSurpriseWait, HandScurryTime, EndTimeWait;

    public Transform HandDestination;

    public float speedDif;


    public void StartScene()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        PlayerCam.SetActive(false);
        yield return new WaitForSeconds(InitWait);
        HandAnim.SetTrigger("Surprise");
        yield return new WaitForSeconds(HandSurpriseWait);
        HandAgent.SetDestination(HandDestination.position);
        float currentTime = 0;
        while (currentTime < HandScurryTime)
        {
            currentTime += Time.deltaTime;
            HandAnim.SetFloat("Speed", HandAgent.velocity.magnitude);
            HandAnim.SetFloat("Direction", GetDirection());
            HandAnim.speed = HandAgent.velocity.magnitude * speedDif;
            if (HandAnim.speed < 1)
            {
                HandAnim.speed = 1;
            }
            yield return new WaitForFixedUpdate();
        }
        DoorAnim.SetTrigger("Close");
        yield return new WaitForSeconds(EndTimeWait);
        endEvent.Invoke();
        PlayerCam.SetActive(true);
        SceneCam.SetActive(false);
    }
    
    public virtual float GetDirection()
    {
        float angle =  GeneralFunctions.GetDirection(HandAnim.velocity + HandAnim.transform.position, HandAnim.transform.position);
        angle /= 360;
        angle += .5f;
        return angle;
    }
}
