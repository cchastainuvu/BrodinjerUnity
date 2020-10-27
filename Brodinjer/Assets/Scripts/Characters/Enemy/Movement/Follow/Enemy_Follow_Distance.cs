using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/Follow/Distance")]

public class Enemy_Follow_Distance : Enemy_Follow_Base
{
    public float DistanceFromFollow;
     public bool lookAtFollow;
    private Vector3 target, destinationTarget;
    private Quaternion facingDirection;
    public float offset;
    
    public override IEnumerator Move()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        if (lookAtFollow)
            agent.updateRotation = false;
        if(animation!= null)
            animation.StartAnimation();
        while (moving)
        {
            if (lookAtFollow && moving)
            {
                target = followObj.transform.position;
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
                destinationTarget = followObj.transform.position;
                destinationTarget += -DistanceFromFollow * agent.transform.forward;
                agent.destination = destinationTarget;
            }

            yield return new WaitForFixedUpdate();
        }
        animation.StopAnimation();
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Follow_Distance temp = CreateInstance<Enemy_Follow_Distance>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.lookAtFollow = lookAtFollow;
        temp.animation = animation;
        temp.offset = offset;
        temp.DistanceFromFollow = DistanceFromFollow;
        return temp;
    }
}
