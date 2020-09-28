using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Attack/Melee")]
public class Enemy_Attack_Melee : Enemy_Attack_Base
{
    public override IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackStartTime);
        meleeAttackObj.SetActive(true);
        yield return new WaitForSeconds(AttackActiveTime);
        meleeAttackObj.SetActive(false);
        yield return new WaitForSeconds(CoolDownTime);

    }

    public override Enemy_Attack_Base getClone()
    {
        Enemy_Attack_Melee temp = CreateInstance<Enemy_Attack_Melee>();
        temp.AttackActiveTime = AttackActiveTime;
        temp.CoolDownTime = CoolDownTime;
        temp.DamageAmount = DamageAmount;
        temp.AttackStartTime = AttackStartTime;
        return temp;
    }
}
