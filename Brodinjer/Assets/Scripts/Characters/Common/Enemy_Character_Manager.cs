using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Character_Manager : Character_Manager
{
    public Enemy_Manager enemyManager;


    public override IEnumerator Stun(float stuntime)
    {
        enemyManager.Stun();
        yield return new WaitForSeconds(stuntime);
        enemyManager.UnStun();
        stunned = false;
    }
}
