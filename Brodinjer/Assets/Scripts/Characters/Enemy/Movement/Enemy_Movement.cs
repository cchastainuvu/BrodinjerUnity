using System.Collections;
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
    private ResetTriggers resetAnims;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        if (anim != null)
        {
            resetAnims = anim.GetComponent<ResetTriggers>();
        }
        if (AnimationBase != null)
        {
            Animation_Base temp = AnimationBase.GetClone();
            AnimationBase = temp;
            AnimationBase.Init(this, anim, player, null);
        }
        Init();
    }

    //Basic Init
    protected virtual void Init()
    {
        canMove = true;
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
            if(resetAnims)
                resetAnims.ResetAllTriggers();
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
