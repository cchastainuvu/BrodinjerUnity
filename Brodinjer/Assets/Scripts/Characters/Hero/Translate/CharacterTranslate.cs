﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
public abstract class CharacterTranslate : ScriptableObject
{
    public float ForwardSpeed, SideSpeed, RunForwardSpeed, RunSideSpeed, JumpSpeed, Gravity;
    public float AnimSpeedMax;
    protected float currentForwardSpeed, currentSideSpeed;
    protected float forwardAmount, sideAmount, headingAngle, vSpeed;
    protected Transform Camera;
    protected CharacterController _cc;
    [HideInInspector]
    public bool canMove, canRun, extraControlled;
    private Coroutine moveFunc, runFunc;
    protected Targeting targetScript;
    public Animation_Base animation;
    protected MonoBehaviour caller;
    public string HorizontalAxis = "Horizontal", VerticalAxis = "Vertical", JumpAxis = "Jump";
    protected Animator anim;
    protected ResetTriggers reset;

    public virtual void Init(MonoBehaviour caller, CharacterController charc, Transform camera, Targeting targetScript, Animator animator)
    {
        extraControlled = false;
        this.caller = caller;
        this._cc = charc;
        Camera = camera;
        this.targetScript = targetScript;
        this.anim = animator;
        reset = anim.GetComponent<ResetTriggers>();
        if(animation!= null)
            animation.Init(caller, animator, _cc.transform, null);
    }

    
    public abstract IEnumerator Move();

    public abstract IEnumerator Run();

    public abstract float getMoveAngle();

    public abstract float getSpeed();
    
    protected float ConvertRange(float origMinRange, float origMaxRange, float newMinRange, float newMaxRange, float value)
    {
        return (value - origMinRange) * (newMaxRange - newMinRange) / (origMaxRange - origMinRange) + newMinRange;
    }

    //public abstract void Invoke(float forwardSpeed, float sideSpeed, bool jump);


}
