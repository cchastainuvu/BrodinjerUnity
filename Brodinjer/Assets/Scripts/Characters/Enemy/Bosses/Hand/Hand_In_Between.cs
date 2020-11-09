using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Boss/Hand/Movement/InbetweenFollow")]
public class Hand_In_Between : Enemy_Follow_Base
{
    public float distanceFromFollowObj;
    public bool lookAtFollow;
    private Quaternion facingDirection;
    private Vector3 followDest, lookDirection;
    public bool FollowObjMain;
    private Vector3 target;
    public float offset;
    
    public override IEnumerator Move()
    {
        agent.speed = Speed;
        agent.updatePosition = true;
        while (moving)
        {
            agent.updateRotation = true;
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

            if (FollowObjMain)
            {
                followDest = followObj.transform.position;
                followDest += destinations[0].transform.forward * -distanceFromFollowObj;
            }
            else
            {
                followDest = destinations[0].transform.position;
                followDest += followObj.transform.forward * -distanceFromFollowObj;
            }

            if (agent.enabled)
                agent.destination = followDest;
            yield return new WaitForFixedUpdate();
        }
    }

    public override Enemy_Movement GetClone()
    {
        Hand_In_Between temp = CreateInstance<Hand_In_Between>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.distanceFromFollowObj = distanceFromFollowObj;
        temp.lookAtFollow = lookAtFollow;
        temp.FollowObjMain = FollowObjMain;
        temp.animation = animation;
        return temp;
    }
}
