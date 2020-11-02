using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/NavMesh/BackStep")]
public class Enemy_Back_Step : NavMesh_Enemy_Base
{
    public float distanceAway;
    public bool turnAway;
    
    protected override void Init(GameObject enemy, MonoBehaviour caller, Transform FollowObj, Animator anim)
    {
        base.Init(enemy, caller, FollowObj, anim);
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
                agent.destination = followObj.transform.position + (agent.transform.forward * -distanceAway);
            yield return fixedUpdate;
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
