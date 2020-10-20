using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character/Enemy/Movement/Follow/LookAt")]
public class Enemy_Look_At_Basic : Enemy_Follow_Base
{
    private Quaternion facingDirection;
    public bool AlwaysReturn;
    private Vector3 followPos, agentPos, destinationPos;

    public override IEnumerator Move()
    {
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
                destinationPos = destinations[0].position;
                destinationPos.y = agent.transform.position.y;
                agent.destination = destinationPos;
            }

            followPos = followObj.transform.position;
            followPos.y = 0;
            agentPos = agent.transform.position;
            agentPos.y = 0;
            facingDirection = Quaternion.LookRotation((followPos - agentPos).normalized);
            agent.transform.rotation =
                Quaternion.Lerp(agent.transform.rotation, facingDirection, AngularSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Look_At_Basic temp = CreateInstance<Enemy_Look_At_Basic>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.AlwaysReturn = AlwaysReturn;
        temp.animation = animation;
        return temp;
    }

}
