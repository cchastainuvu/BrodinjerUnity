using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Attack_Base : ScriptableObject
{
    public float DamageAmount;
    public float AttackStartTime;
    public float CoolDownTime;
    protected bool attacking;
    public float AttackActiveTime;
    protected GameObject meleeAttackObj;
    protected Coroutine attackFunc;
    protected MonoBehaviour caller;
    public Animation_Base animations;

    public virtual void Init(MonoBehaviour caller, GameObject MeleeAttack, Transform player, Animator animator)
    {
        this.caller = caller;
        meleeAttackObj = MeleeAttack;
        if(animations != null)
            animations.Init(caller, animator, player);
    }

    public virtual void StartAttack()
    {
        attacking = true;
        attackFunc = caller.StartCoroutine(Attack());
    }
    
    public abstract IEnumerator Attack();

    public virtual void StopAttack()
    {
        attacking = false;
        if(attackFunc!= null)
            caller.StopCoroutine(attackFunc);
        meleeAttackObj.SetActive(false);
    }

    public abstract Enemy_Attack_Base getClone();
}
