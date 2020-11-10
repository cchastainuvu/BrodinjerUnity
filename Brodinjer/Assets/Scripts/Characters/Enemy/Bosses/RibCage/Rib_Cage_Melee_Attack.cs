using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Rib_Cage_Melee_Attack : Enemy_Attack_Base
{
    private bool currentlyattacking;

    public override void Init()
    {
        base.Init();
        WeaponAttackobj.SetActive(false);
    }

    public override IEnumerator Attack()
    {
        currentlyattacking = false;
            if (!currentlyattacking)
            {
                //Debug.Log("Run Anims");
                if (animations != null)
                    animations.StartAnimation();
                currentlyattacking = true;
                yield return new WaitForSeconds(AttackStartTime);
                WeaponAttackobj.SetActive(true);
                yield return new WaitForSeconds(AttackActiveTime);
                WeaponAttackobj.SetActive(false);
                yield return new WaitForSeconds(CoolDownTime);
                currentlyattacking = false;
            }
            yield return new WaitForFixedUpdate();
    }

    /*public override Enemy_Attack_Base getClone()
    {
        Rib_Cage_Melee_Attack temp = CreateInstance<Rib_Cage_Melee_Attack>();
        temp.AttackActiveTime = AttackActiveTime;
        temp.AttackStartTime = AttackStartTime;
        temp.CoolDownTime = CoolDownTime;
        temp.animations = animations;
        temp.attackWhileMoving = attackWhileMoving;

        return temp;
    }*/
}
