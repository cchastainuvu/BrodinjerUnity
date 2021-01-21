using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_King_Phase_01 : Phase_Base
{
    /*
     *dragging his hand violently along the arena in one fell swipe
     * or just trying to directly slam his fist down on Malik
     * He will occasionally use magic by slamming a splayed, glowing hand to the ground.
     *      Select pieces of debris will begin to glow and scale down
     */

    public List<Enemy_Attack_Base> PhaseAttacks;
    private int randAttack;
    private Enemy_Attack_Base attack;
    public float minInBetween, maxInBetween;
    private float waitTime;

    public override void StartPhase()
    {
        if (!currentPhase)
        {
            currentPhase = true;
            phaseFunc = StartCoroutine(RunPhase());
        }
    }

    public override IEnumerator RunPhase()
    {
        while (currentPhase)
        {
            //Wait for Time
            waitTime = Random.Range(minInBetween, maxInBetween);
            yield return new WaitForSeconds(waitTime);
            //Randomize Attack
            randAttack = Random.Range(0, PhaseAttacks.Count);
            attack = PhaseAttacks[randAttack];
            attack.attacking = true;
            yield return StartCoroutine(attack.Attack());
        }
    }

    public override void StopPhase()
    {
        if(phaseFunc != null)
            StopCoroutine(phaseFunc);
        currentPhase = false;
    }

    public override void StopAttack()
    {
        if (attack != null)
            attack.attacking = false;
    }
}
