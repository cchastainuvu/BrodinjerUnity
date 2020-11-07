using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Player/Translate/ThirdPerson")]
public class ThirdPersonTranslate : CharacterTranslate
{
    
    private Vector3 _moveVec, _jumpVec;
    private bool invoking, jumping, falling;
    private float currentTime;
    public string JumpTrigger;
    public float JumpDelay;
    private readonly WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    public float DodgeDelay;
    private bool dodging = false;
    public float DodgeTime, DodgeAmount;
    private Vector3 DodgeDirection;
    public Animation_Base DodgeAnimations;

    
    
    public override void Init(MonoBehaviour caller, CharacterController _cc, Transform camera, Targeting target, Animator animator)
    {
        base.Init(caller, _cc, camera, target, animator);
        if(DodgeAnimations!= null)
            DodgeAnimations.Init(caller, animator, _cc.transform, null);
        invoking = false;
        falling = false;
        dodging = false;
        jumping = false;
        _moveVec = Vector3.zero;
        currentForwardSpeed = ForwardSpeed;
        currentSideSpeed = SideSpeed;
        
        
    }


    public override IEnumerator Move()
    {
        if(animation != null)
            animation.StartAnimation();
        while (canMove)
        {
            if (!invoking && canMove)
            {
                invoking = true;
                caller.StartCoroutine(Invoke());
            }

            if (!canMove)
            {
                vSpeed -= Gravity * Time.deltaTime;
                _moveVec = Vector3.zero;
                _moveVec.y = vSpeed;
                _cc.Move(_moveVec * Time.deltaTime);
            }

            yield return fixedUpdate;
        }

    }

    public override IEnumerator Run()
    {
        while (true)
        {
            if (canRun)
            {
                if (Input.GetButton("Sprint"))
                {
                    currentForwardSpeed = RunForwardSpeed;
                    currentSideSpeed = RunSideSpeed;
                }
                else
                {
                    currentForwardSpeed = ForwardSpeed;
                    currentSideSpeed = SideSpeed;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public override float getMoveAngle()
    {        
        return GetDirection(_cc.transform.InverseTransformDirection(_cc.velocity));
    }

    Vector3 GetLocalDirection()
    {
        return _cc.transform.InverseTransformDirection(Input.GetAxisRaw(HorizontalAxis)*_cc.transform.right + Input.GetAxisRaw(VerticalAxis)*_cc.transform.forward);
    }
    
    public virtual float GetDirection(Vector3 moveDirection)
    {
        Vector3 collisionposition = moveDirection;
        collisionposition.y = 0;
        Vector3 transformposition = Vector3.zero;
        transformposition.y = 0;
        Vector3 target = collisionposition - transformposition;
        float angle = Vector3.Angle(target, Vector3.forward);
        Vector3 crossProduct = Vector3.Cross(target, Vector3.forward);
        if (crossProduct.y < 0)
        {
            angle = -angle;
        }

        angle /= 360;
        angle += .5f;
        return angle;
    }

    public override float getSpeed()
    {
        return ConvertRange(0, AnimSpeedMax, 0, 1, _cc.velocity.magnitude);
    }

    public virtual IEnumerator Invoke()
    {
        _moveVec = Camera.forward * currentForwardSpeed * Input.GetAxis("Vertical") +
                   Camera.right * currentSideSpeed * Input.GetAxis("Horizontal");
        _moveVec.y = 0;
        if (_cc.isGrounded) {
            vSpeed = -10;
            if (!jumping && !falling && (Input.GetButtonDown ("Jump"))) {
                if (!dodging && targetScript.targeting && (Input.GetButton(HorizontalAxis) || Input.GetButton(VerticalAxis)))
                {
                    if(DodgeAnimations!= null)
                        DodgeAnimations.StartAnimation();
                    yield return new WaitForSeconds(DodgeDelay);
                    dodging = true;
                    DodgeDirection = _cc.transform.forward*Input.GetAxisRaw(VerticalAxis) + _cc.transform.right*Input.GetAxisRaw(HorizontalAxis);
                    currentTime = 0;
                    while (currentTime < DodgeTime)
                    {
                        _moveVec = DodgeDirection * DodgeAmount;
                        _moveVec.y = 0;
                        _cc.Move(_moveVec * Time.deltaTime);
                        currentTime += Time.deltaTime;
                        yield return fixedUpdate;
                    }
                    dodging = false;
                }
                else
                {
                    jumping = true;
                    if(reset)
                        reset.ResetAllTriggers();
                    anim.SetTrigger(JumpTrigger);
                    currentTime = 0;
                    while (currentTime < JumpDelay)
                    {
                        vSpeed -= Gravity * Time.deltaTime;
                        _moveVec.y = vSpeed;
                        _cc.Move(_moveVec * Time.deltaTime);
                        currentTime += Time.deltaTime;
                        yield return fixedUpdate;
                    }
                    vSpeed = JumpSpeed;
                }
            }
            else
            {
                if ((jumping || falling || anim.GetBool("Fall")))
                {
                    falling = false;
                    jumping = false;
                    if(reset)
                        reset.ResetAllTriggers();
                    anim.SetTrigger("Land");
                    anim.SetBool("Fall", false);
                }
            }
        }
        else
        {
            if (!jumping && !falling)
            {
                falling = true;
                if(reset)
                    reset.ResetAllTriggers();
                anim.SetBool("Fall", true);
            }
        }
        vSpeed -= Gravity * Time.deltaTime;
        _moveVec.y = vSpeed;
        _cc.Move(_moveVec * Time.deltaTime);
        invoking = false;
        yield return null;
    }

}
