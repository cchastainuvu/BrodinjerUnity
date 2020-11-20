using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_Movement : MonoBehaviour
{
    public float Speed;
    public float AngularSpeed = 120;
    protected bool moving;
    public Transform enemy;
    protected Transform player;
    protected bool idle;
    public Animation_Base AnimationBase;
    protected Coroutine moveFunc;
    protected bool canMove;
    protected readonly WaitForFixedUpdate fixedUpdate= new WaitForFixedUpdate();
    public Animator anim;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        Init();
    }

    //Basic Init
    protected virtual void Init()
    {
        canMove = true;
        if (AnimationBase != null)
        {
            AnimationBase.Init(this, anim, player, null);
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

    public virtual void StartMove()
    {
        if (canMove)
        {
            moving = true;
            moveFunc = StartCoroutine(Move());
            if(AnimationBase)
                AnimationBase.StartAnimation();
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
            StopCoroutine(moveFunc);
        }
        if(AnimationBase)
            AnimationBase.StopAnimation();
    }


}
