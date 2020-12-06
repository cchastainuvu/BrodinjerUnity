using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Attack_Melee_Taunt : Enemy_Attack_Base
{
    public List<float> attackPercentages;
    public List<bool> tauntBools;
    private float randomNum;
    public string animationFloat;
    public List<float> attackActiveTimes;
    private int currentnum;
    
    public override IEnumerator Attack()
    {
        GetRandom();
        animations.StartAnimation();
        yield return new WaitForSeconds(AttackStartTime);
        currentnum = GetRandom();
        yield return new WaitForSeconds(attackActiveTimes[currentnum]);
        WeaponAttackobj.SetActive(false);
        animations.StopAnimation();
        yield return new WaitForSeconds(CoolDownTime);

    }

    private int GetRandom()
    {
        randomNum = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < attackPercentages.Count-1; i++)
        {
            if (randomNum >= (attackPercentages[i]) && randomNum < attackPercentages[i + 1])
            {
                if (!tauntBools[i])
                {
                    WeaponAttackobj.SetActive(true);
                }
                animator.SetFloat(animationFloat, (i/(attackPercentages.Count-1)));
                return i;
            }
        }
        if(!tauntBools[tauntBools.Count-1])
            WeaponAttackobj.SetActive(true);
        animator.SetFloat(animationFloat, 1);
        return attackPercentages.Count-1;

    }
}
