using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForcePlayerWalk : MonoBehaviour
{
    private Vector3 _moveVec;
    private readonly WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    public Animator anim;
    private ResetTriggers resetAnims;
    private CharacterController _cc;
    public float ForwardSpeed;
    private bool moving;
    private Coroutine moveFunc;
    public float AnimSpeedMax;

    public Transform Target;

    public UnityEvent ReachDestEvent;

    public bool AwakeOnStart;

    public string StartTrigger, EndTrigger, SpeedFloat, DirectionFloat;

    public Animation_Base animbase;
    
    private void Start()
    {
        //Debug.Log("Camera: " + camera.gameObject.name);
        moving = false;
        _cc = GetComponent<CharacterController>();
        resetAnims = anim.GetComponent<ResetTriggers>();
        _moveVec = Vector3.zero;
        if(AwakeOnStart)
            StartMove();
    }

    public void StartMove()
    {
        if (!moving)
        {
            moving = true;
            moveFunc = StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        animbase.StopAnimation();
        _moveVec = Target.position - transform.position;
        _moveVec.y = 0;
        if(resetAnims)
            resetAnims.ResetAllTriggers();
        anim.SetTrigger(StartTrigger);
        while (_moveVec.magnitude > .1f && moving)
        {
            Debug.Log("Force Move");
            _moveVec = _moveVec.normalized * ForwardSpeed;
            anim.SetFloat(SpeedFloat, 1);
            anim.SetFloat(DirectionFloat, getMoveAngle());
            _cc.Move(_moveVec * Time.deltaTime);
            _moveVec = Target.position - transform.position;
            _moveVec.y = 0;
            yield return fixedUpdate;
        }
        ReachDestEvent.Invoke();
        anim.SetFloat(SpeedFloat, 0);
        Debug.Log("End Move");
        moving = false;
    }

    public void StopMove()
    {
        if(moveFunc!= null)
            StopCoroutine(moveFunc);
        if(resetAnims)
            resetAnims.ResetAllTriggers();
        if(EndTrigger != "")
            anim.SetTrigger(EndTrigger);
        moving = false;
    }

    public float getMoveAngle()
    {        
        return GetDirection(_cc.transform.InverseTransformDirection(_cc.velocity));
    }
    
    public float GetDirection(Vector3 moveDirection)
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

    public float getSpeed()
    {
        return GeneralFunctions.ConvertRange(0, AnimSpeedMax, 0, 1, _cc.velocity.magnitude);
    }
}
