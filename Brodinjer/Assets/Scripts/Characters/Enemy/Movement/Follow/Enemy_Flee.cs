using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Flee : NavMesh_Enemy_Base
{
 
     private NavMeshAgent myNMagent;
     private float nextTurnTime;
     private Transform startTransform;
 
    public override IEnumerator Move()
    {
        while (moving)
        {
            startTransform = agent.transform;
            agent.transform.rotation = Quaternion.LookRotation(agent.transform.position - player.position);
            Vector3 runTo = agent.transform.position + agent.transform.forward * Speed;
            NavMeshHit hit;
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
            nextTurnTime = Time.time + 5;
            agent.transform.position = startTransform.position;
            agent.transform.rotation = startTransform.rotation;
            myNMagent.SetDestination(hit.position);
            yield return fixedUpdate;
        }
    }

    /*public override Enemy_Movement GetClone()
    {
        Enemy_Flee temp = CreateInstance<Enemy_Flee>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        return temp;
    }*/
}
