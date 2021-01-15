﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hand_In_Between : Enemy_Follow_Base
{
    public float distanceFromFollowObj;
    public bool lookAtFollow;
    private Quaternion facingDirection;
    private Vector3 followDest, lookDirection;
    public bool FollowObjMain;
    private Vector3 target;
    public float offset;
    public Transform Destination01;
    
    public override IEnumerator Move()
    {
        agent.speed = Speed;
        agent.updatePosition = true;
        while (moving)
        {
            agent.updateRotation = true;
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

            if (FollowObjMain)
            {
                followDest = player.transform.position;
                followDest += Destination01.transform.forward * -distanceFromFollowObj;
            }
            else
            {
                followDest = Destination01.transform.position;
                followDest += player.transform.forward * -distanceFromFollowObj;
            }

            if (agent.enabled)
                agent.destination = followDest;
            yield return new WaitForFixedUpdate();
        }
    }

}
