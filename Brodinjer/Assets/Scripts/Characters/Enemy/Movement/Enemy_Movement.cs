using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_Movement : ScriptableObject
{
    public float Speed;
    protected bool moving;
    protected NavMeshAgent agent;
    private Coroutine moveFunc;
    protected bool idle;
    protected List<Transform> destinations;
    protected Transform enemy;
    protected MonoBehaviour caller;
    protected Transform followObj;
    private bool canMove;

    //Basic Init
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller)
    {
        canMove = true;
        this.agent = agent;
        this.agent.speed = 0;
        enemy = agent.transform;
        this.caller = caller;
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
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller, List<Transform> destinations)
    {
        Init(agent, caller);
        this.destinations = destinations;
    }
    
    //Follow Init
    protected virtual void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj)
    {
        Init(agent, caller);
        this.followObj = FollowObj;
    }
    
    //Usable Init
    public virtual void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj,
        List<Transform> destinations)
    {
        Init(agent, caller);
        Init(agent, caller, FollowObj);
        Init(agent, caller, destinations);
    }
    

    public virtual void StartMove()
    {
        if (canMove)
        {
            agent.speed = Speed;
            moving = true;
            moveFunc = caller.StartCoroutine(Move());
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
        Debug.Log("Stop Move Enemy Movement");
    }

    public abstract Enemy_Movement GetClone();

}
