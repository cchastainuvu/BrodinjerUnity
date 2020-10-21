using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/BackStep")]
public class Enemy_Back_Step : Enemy_Movement
{
    public float distanceAway;
    public bool turnAway;
    
    protected override void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj, Animator anim)
    {
        base.Init(agent, caller, FollowObj, anim);
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
        while (moving)
        {
            agent.destination = followObj.transform.position + (agent.transform.forward * -distanceAway);
            yield return new WaitForFixedUpdate();
        }
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Back_Step temp = CreateInstance<Enemy_Back_Step>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.distanceAway = distanceAway;
        temp.turnAway = turnAway;
        return temp;
    }
}
