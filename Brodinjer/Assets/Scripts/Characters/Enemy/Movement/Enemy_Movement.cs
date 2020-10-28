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
    protected GameObject enemy;
    protected Transform followObj;
    protected bool idle;
    public Animation_Base animation;
    protected MonoBehaviour caller;
    protected List<Transform> destinations;
    protected Coroutine moveFunc;
    protected bool canMove;
    protected readonly WaitForFixedUpdate fixedUpdate= new WaitForFixedUpdate();

    //Basic Init
    protected virtual void Init(GameObject enemy, MonoBehaviour caller, Animator anim)
    {
        canMove = true;
        this.enemy = enemy;
        this.caller = caller;
        if (animation != null)
        {
            animation.Init(caller, anim, followObj, null);
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
    protected virtual void Init(GameObject enemy, MonoBehaviour caller, List<Transform> destinations, Animator anim)
    {
        Init(enemy, caller, anim);
        this.destinations = destinations;
    }
    
    //Follow Init
    protected virtual void Init(GameObject enemy, MonoBehaviour caller, Transform FollowObj, Animator anim)
    {
        Init(enemy, caller, anim);
        this.followObj = FollowObj;
    }
    
    //Usable Init
    public virtual void Init(GameObject enemy, MonoBehaviour caller, Transform FollowObj,
        List<Transform> destinations, Animator anim)
    {
        Init(enemy, caller, anim);
        Init(enemy, caller, FollowObj, anim);
        Init(enemy, caller, destinations, anim);
    }
    

    public virtual void StartMove()
    {
        if (canMove)
        {
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
