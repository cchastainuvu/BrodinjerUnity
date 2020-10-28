using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character/Enemy/Boss/Hand/Attack/Jump Attack")]
public class Hand_Jump_Attack : Enemy_Attack_Base
{
    public float UpwardForce, ForwardForce;
    private Rigidbody enemyRigid;
    private Vector3 jumpdirection;
    public float InBetweenAttackTime;

    public override void Init(MonoBehaviour caller, GameObject MeleeAttack, Transform player, Animator animator, GameObject enemy)
    {
        base.Init(caller, MeleeAttack, player, animator, enemy);
        enemyRigid = enemyObj.GetComponent<Rigidbody>();
        if (!enemyRigid)
            enemyRigid = enemyObj.AddComponent<Rigidbody>();
    }

    public override void StartAttack()
    {
        if (!attacking)
        {
            attacking = true;
            caller.StartCoroutine(Attack());
        }
    }

    public override IEnumerator Attack()
    {
        
            yield return new WaitForSeconds(AttackStartTime);

        enemyObj.GetComponent<NavMeshAgent>().enabled = false;
        jumpdirection = (enemyRigid.transform.up * UpwardForce) + (enemyRigid.transform.forward * ForwardForce);
        enemyRigid.AddForce(jumpdirection, ForceMode.Impulse);
        if (WeaponAttackobj)
            WeaponAttackobj.SetActive(true);
        yield return new WaitForSeconds(AttackActiveTime);
        if (WeaponAttackobj)
            WeaponAttackobj.SetActive(false);
        yield return new WaitForSeconds(CoolDownTime);
        enemyObj.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(InBetweenAttackTime);
        attacking = false;
    }

    public override Enemy_Attack_Base getClone()
    {
        Hand_Jump_Attack temp = CreateInstance<Hand_Jump_Attack>();
        temp.AttackActiveTime = AttackActiveTime;
        temp.CoolDownTime = CoolDownTime;
        temp.DamageAmount = DamageAmount;
        temp.AttackStartTime = AttackStartTime;
        temp.animations = animations;
        temp.UpwardForce = UpwardForce;
        temp.ForwardForce = ForwardForce;
        temp.InBetweenAttackTime = InBetweenAttackTime;
        temp.attackWhileMoving = attackWhileMoving;

        return temp;
    }
}
