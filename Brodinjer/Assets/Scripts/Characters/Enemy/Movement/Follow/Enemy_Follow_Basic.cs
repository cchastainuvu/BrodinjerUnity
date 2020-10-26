using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/Follow/Basic")]
public class Enemy_Follow_Basic : Enemy_Follow_Base
{
    public bool lookAtFollow;
    private Vector3 target;
    private Quaternion facingDirection;
    public float offset;
    
    public override IEnumerator Move()
    {
        //agent.speed = Speed;
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
                    /*Quaternion YRotation = Quaternion.Euler(((lookatX) ? facingDirection.eulerAngles.x :agent.transform.rotation.eulerAngles.x), 
                        ((lookatY) ? facingDirection.eulerAngles.y :agent.transform.rotation.eulerAngles.y), 
                        ((lookatZ) ? facingDirection.eulerAngles.z :agent.transform.rotation.eulerAngles.z));*/
                    agent.transform.rotation =
                        Quaternion.Lerp(agent.transform.rotation, YRotation, AngularSpeed * Time.deltaTime);
                }
            }
            if (agent.enabled)
            {
                agent.destination = followObj.transform.position;
            }

            yield return new WaitForFixedUpdate();
        }
        animation.StopAnimation();
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Follow_Basic temp = CreateInstance<Enemy_Follow_Basic>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.lookAtFollow = lookAtFollow;
        temp.animation = animation;
        return temp;
    }
}
