using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Attack_Melee_Taunt : Enemy_Attack_Base
{
    public float percentageTaunt;
    private float randomNum;
    public string animationFloat;
    
    public override IEnumerator Attack()
    {
        GetRandom();
        animations.StartAnimation();
        yield return new WaitForSeconds(AttackStartTime);
        if(randomNum > percentageTaunt)
            WeaponAttackobj.SetActive(true);
        yield return new WaitForSeconds(AttackActiveTime);
        WeaponAttackobj.SetActive(false);
        animations.StopAnimation();
        yield return new WaitForSeconds(CoolDownTime);

    }

    private void GetRandom()
    {
        randomNum = Random.Range(0.0f, 1.0f);
        if(randomNum <= percentageTaunt)
            animator.SetFloat(animationFloat, 1);
        else
            animator.SetFloat(animationFloat, 0);

    }
}
