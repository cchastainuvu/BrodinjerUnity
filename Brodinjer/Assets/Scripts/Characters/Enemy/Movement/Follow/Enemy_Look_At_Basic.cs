using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Look_At_Basic : Enemy_Follow_Base
{
    private Quaternion facingDirection;
    public bool AlwaysReturn;
    private Vector3 followPos, agentPos, destinationPos;
    public Transform Destination;

    public override IEnumerator Move()
    {
        AnimationBase.StartAnimation();
        agent.updateRotation = false;
        if (AlwaysReturn)
        {
            agent.updatePosition = true;
        }
        else
        {
            agent.updatePosition = false;
        }
        while (moving)
        {
            if (AlwaysReturn && agent.enabled)
            {
                destinationPos = Destination.position;
                destinationPos.y = agent.transform.position.y;
                agent.destination = destinationPos;
            }

            followPos = player.transform.position;
            followPos.y = 0;
            agentPos = agent.transform.position;
            agentPos.y = 0;
            facingDirection = Quaternion.LookRotation((followPos - agentPos).normalized);
            agent.transform.rotation =
                Quaternion.Lerp(agent.transform.rotation, facingDirection, AngularSpeed * Time.deltaTime);
            yield return fixedUpdate;
        }
    }


}
