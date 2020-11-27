using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Flee : NavMesh_Enemy_Base
{
     private Transform startTransform;
 
    public override IEnumerator Move()
    {
        while (moving)
        {
            startTransform = agent.transform;
            agent.transform.rotation = Quaternion.LookRotation(agent.transform.position - player.position);
            Vector3 runTo = agent.transform.position + agent.transform.forward * Speed;
            NavMeshHit hit;
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Default")); 
            agent.transform.position = startTransform.position;
            agent.transform.rotation = startTransform.rotation;
            agent.SetDestination(hit.position);
            yield return fixedUpdate;
        }
    }

}
