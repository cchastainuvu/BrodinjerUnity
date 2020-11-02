using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NavMesh_Enemy_Base : Enemy_Movement
{
    [HideInInspector]
    public NavMeshAgent agent;

    //Basic Init
    protected override void Init(GameObject enemy, MonoBehaviour caller, Animator anim)
    {
        canMove = true;
        this.enemy = enemy;
        this.agent = enemy.GetComponent<NavMeshAgent>();
        if (this.agent == null)
        {
            this.agent = enemy.AddComponent<NavMeshAgent>();
        }
        this.agent.speed = 0;
        this.agent.angularSpeed = AngularSpeed;
        this.caller = caller;
        if (animation != null)
        {
            animation.Init(caller, anim, followObj, agent);
        }
    }
    
    public override void StopMove()
    {
        moving = false;
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        if (moveFunc != null)
        {
            caller.StopCoroutine(moveFunc);
        }
        if(animation)
            animation.StopAnimation();
        //Debug.Log("Stop Move Enemy Movement");
    }

}
