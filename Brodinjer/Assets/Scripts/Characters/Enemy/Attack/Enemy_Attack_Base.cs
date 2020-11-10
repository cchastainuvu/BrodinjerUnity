using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_Attack_Base : MonoBehaviour
{
    public float AttackStartTime;
    public float CoolDownTime;
    protected bool attacking;
    public float AttackActiveTime;
    public GameObject WeaponAttackobj;
    protected Coroutine attackFunc;
    public Animation_Base animations;
    public GameObject enemyObj;
    public bool attackWhileMoving;
    protected Transform player;
    protected bool canAttack;
    public Animator animator;
    public float MovePauseTime;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        Init();
    }

    public virtual void Init()
    {
        attacking = false;
        canAttack = true;
        if(animations != null && animator != null)
            animations.Init(this, animator, player, GetComponent<NavMeshAgent>());
    }

    public void ActivateAttack()
    {
        canAttack = true;
    }

    public void DeactivateAttack()
    {
        canAttack = false;
        StopAttack();
    }

    public virtual void StartAttack()
    {
        if (canAttack)
        {
            attacking = true;
            attackFunc = StartCoroutine(Attack());
        }
    }
    
    public abstract IEnumerator Attack();

    public virtual void StopAttack()
    {
        attacking = false;
        if(attackFunc!= null)
            StopCoroutine(attackFunc);
        if(WeaponAttackobj!= null)
            WeaponAttackobj.SetActive(false);
    }

    /*public abstract Enemy_Attack_Base getClone();*/
}
