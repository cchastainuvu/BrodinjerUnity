using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterTranslate : ScriptableObject
{
    public float ForwardSpeed, SideSpeed, RunForwardSpeed, RunSideSpeed, JumpSpeed, Gravity;
    protected float currentForwardSpeed, currentSideSpeed;
    protected float forwardAmount, sideAmount, headingAngle, vSpeed;
    public string ForwardAxis, SideAxis;
    protected Transform Camera;
    protected CharacterController _cc;
    [HideInInspector]
    public bool canMove, canRun;
    private Coroutine moveFunc, runFunc;
    protected Targeting targetScript;
    public Animation_Base animation;
    protected MonoBehaviour caller;


    public virtual void Init(MonoBehaviour caller, CharacterController _cc, Transform camera, Targeting targetScript, Animator animator)
    {
        this.caller = caller;
        this._cc = _cc;
        Camera = camera;
        this.targetScript = targetScript;
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


}
