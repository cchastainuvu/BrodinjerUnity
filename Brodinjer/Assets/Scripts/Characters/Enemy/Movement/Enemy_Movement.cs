using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_Movement : ScriptableObject
{
    public float Speed;
    public float AngularSpeed = 120;
    protected bool moving;
    protected NavMeshAgent agent;
    private Coroutine moveFunc;
    protected bool idle;
    protected List<Transform> destinations;
    protected Transform enemy;
    protected MonoBehaviour caller;
    protected Transform followObj;
    private bool canMove;
    public Animation_Base animation;

    //Basic Init
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller, Animator anim)
    {
        canMove = true;
        this.agent = agent;
        this.agent.speed = 0;
        this.agent.angularSpeed = AngularSpeed;
        enemy = agent.transform;
        this.caller = caller;
        if (animation != null)
        {
            animation.Init(caller, anim, followObj, agent);
        }
    }

    public void deactiveMove()
    {
        canMove = false;
        StopMove();
    }

    public void activateMove()
    {
        canMove = true;
    }
    
    //Patrol Init
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller, List<Transform> destinations, Animator anim)
    {
        Init(agent, caller, anim);
        this.destinations = destinations;
    }
    
    //Follow Init
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj, Animator anim)
    {
        Init(agent, caller, anim);
        this.followObj = FollowObj;
    }
    
    //Usable Init
    public virtual void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj,
        List<Transform> destinations, Animator anim)
    {
        Init(agent, caller, anim);
        Init(agent, caller, FollowObj, anim);
        Init(agent, caller, destinations, anim);
    }
    

    public virtual void StartMove()
    {
        if (canMove)
        {
            agent.speed = Speed;
            moving = true;
            moveFunc = caller.StartCoroutine(Move());
            if(animation)
                animation.StartAnimation();
        }
        else
        {
            StopMove();
        }

    }

    public abstract IEnumerator Move();

    public virtual void StopMove()
    {
        agent.speed = 0;
        moving = false;
        if (moveFunc != null)
        {
            caller.StopCoroutine(moveFunc);
        }
        if(animation)
            animation.StopAnimation();
        //Debug.Log("Stop Move Enemy Movement");
    }

    public abstract Enemy_Movement GetClone();

}
