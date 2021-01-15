using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Back_Step : NavMesh_Enemy_Base
{
    public float distanceAway;
    public bool turnAway;
    
    protected override void Init()
    {
        base.Init();
        if (!turnAway)
        {
            agent.updateRotation = false;
        }
        else
        {
            agent.updateRotation = true;
        }
    }

    public override IEnumerator Move()
    {
        agent.speed = Speed;
        while (moving)
        {
            if(agent.enabled)
                agent.destination = player.transform.position + (agent.transform.forward * -distanceAway);
            yield return fixedUpdate;
        }
    }
}
