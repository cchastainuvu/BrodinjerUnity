using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/Follow/Basic")]
public class Enemy_Follow_Basic : Enemy_Follow_Base
{
    public override IEnumerator Move()
    {
        //agent.speed = Speed;
        agent.updateRotation = true;
        agent.updatePosition = true;
        while (moving)
        {
            if (agent.enabled)
            {
                agent.destination = followObj.transform.position;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Follow_Basic temp = CreateInstance<Enemy_Follow_Basic>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        return temp;
    }
}
