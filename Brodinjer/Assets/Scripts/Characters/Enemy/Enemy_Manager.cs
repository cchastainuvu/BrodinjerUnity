using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Manager : MonoBehaviour
{
    public Enemy_Movement Movement_Version;
    private Enemy_Movement _movementTemp;
    
    public List<Transform> Destinations;
    private NavMeshAgent agent;

    public bool AttackWhileMoving;
    public GameObject KnockbackObj;
    public Enemy_Attack_Base Attack;
    private Enemy_Attack_Base _attackTemp;

    public Transform Player;
    public Animator animator;

    private bool canAttack;
    private bool canMove;

    private void Start()
    {
        canAttack = true;
        canMove = true;
        agent = GetComponent<NavMeshAgent>();
        Init();
    }

    public void deactivateMove()
    {
        canMove = false;
        Movement_Version.deactiveMove();
        StopMove();
    }

    public void deactivateAttack()
    {
        canAttack = false;
        StopAttack();
    }

    public void activateMove()
    {
        canMove = true;
        Movement_Version.activateMove();
    }

    public void activateAttack()
    {
        canAttack = true;
    }

    #region INIT FUNCTIONS

    public void Init()
    {
        InitMovement();
        InitAttack();
    }

    public void InitMovement()
    {
        _movementTemp = Movement_Version.GetClone();
        Movement_Version = _movementTemp;
        Movement_Version.Init(agent, this, Player, Destinations);
    }

    public void InitAttack()
    {
        _attackTemp = Attack.getClone();
        Attack = _attackTemp;
        Attack.Init(this, KnockbackObj, Player, animator);
    }
    #endregion

    #region SETTER FUNCTIONS

    public void SetNewMovement(Enemy_Movement movement)
    {
        Movement_Version.StopMove();
        Movement_Version = movement;
        InitMovement();
    }

    public void SetNewAttack(Enemy_Attack_Base attack)
    {
        Attack.StopAttack();
        Attack = attack;
        InitAttack();
    }
    
    #endregion

    #region START FUNCTIONS

    public void StartMove()
    {
        if(canMove)
            Movement_Version.StartMove();
    }

    public void StartAttack()
    {
        if (canAttack)
        {
            if (!AttackWhileMoving)
            {
                StartCoroutine(PauseMove());
            }

            Attack.StartAttack();
        }
    }

    private IEnumerator PauseMove()
    {
        Movement_Version.StopMove();
        yield return new WaitForSeconds(Attack.CoolDownTime + Attack.AttackActiveTime + Attack.AttackStartTime);
        Movement_Version.StartMove();
    }

    #endregion

    #region STOP FUNCTIONS

    public void StopMove()
    {
        Movement_Version.StopMove();
    }

    public void StopAttack()
    {
        if (!AttackWhileMoving)
        {
            Movement_Version.StartMove();
        }
        Attack.StopAttack();
    }

    #endregion
    
}
