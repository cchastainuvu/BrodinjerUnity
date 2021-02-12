using System.Collections;
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
    public Animation_Base animation;
    protected MonoBehaviour caller;
    public string HorizontalAxis = "Horizontal", VerticalAxis = "Vertical", JumpAxis = "Jump";
    protected Animator anim;
    protected ResetTriggers reset;
    protected Z_Targeting target;
    protected RandomSoundController JumpSound;

    public void SetWalk(float speed)
    {
        ForwardSpeed = speed;
        SideSpeed = speed;
    }

    public void SetRun(float speed){
        RunForwardSpeed = speed;
        RunSideSpeed = speed;
    }
    public void SetGravity(float gravity)
    {
        Gravity = gravity;
    }

    public virtual void Init(MonoBehaviour caller, CharacterController charc, Transform camera, Z_Targeting target, Animator animator, RandomSoundController jump)
    {
        extraControlled = false;
        this.caller = caller;
        this._cc = charc;
        Camera = camera;
        this.anim = animator;
        this.target = target;
        reset = anim.GetComponent<ResetTriggers>();
        if(animation!= null)
            animation.Init(caller, animator, _cc.transform, null);
        this.JumpSound = jump;
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
