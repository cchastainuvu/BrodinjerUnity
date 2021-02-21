using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Follow_Distance : Enemy_Follow_Base
{
    public float DistanceFromFollow;
     public bool lookAtFollow;
    private Vector3 target, destinationTarget;
    private Quaternion facingDirection;
    public float offset;
    public SoundController walkSound;
    public float MaxFootstep, MinFootstep;
    public override IEnumerator Move()
    {
        StartCoroutine(WalkSound());
        agent.speed = Speed;
        agent.updateRotation = true;
        agent.updatePosition = true;
        if (lookAtFollow)
            agent.updateRotation = false;
        if(AnimationBase!= null)
            AnimationBase.StartAnimation();
        while (moving)
        {
            if (lookAtFollow && moving)
            {
                target = player.transform.position;
                target = (target - agent.transform.position).normalized;
                facingDirection = Quaternion.LookRotation(target);
                Quaternion YRotation = Quaternion.Euler(agent.transform.rotation.eulerAngles.x,
                    facingDirection.eulerAngles.y, agent.transform.rotation.eulerAngles.z);
                if (!GeneralFunctions.CheckDestination(agent.transform.rotation.eulerAngles,
                    YRotation.eulerAngles, offset))
                {
                    agent.transform.rotation =
                        Quaternion.Lerp(agent.transform.rotation, YRotation, AngularSpeed * Time.deltaTime);
                }
            }
            if (agent.enabled)
            {
                destinationTarget = player.transform.position;
                destinationTarget += -DistanceFromFollow * agent.transform.forward;
                agent.destination = destinationTarget;
            }

            yield return fixedUpdate;
        }
        if(AnimationBase != null)
            AnimationBase.StopAnimation();
    }

    private IEnumerator WalkSound()
    {
        while (moving)
        {
            if (agent.velocity.magnitude >= .1f)
            {
                walkSound.Play();
                yield return new WaitForSeconds(GeneralFunctions.ConvertRange(0, Speed, MaxFootstep, MinFootstep, agent.velocity.magnitude));
            }
            yield return new WaitForFixedUpdate();
        }
    }


}
