using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Attack_None : Enemy_Attack_Base
{
    public override IEnumerator Attack()
    {
        attacking = false;
        yield return null;
    }

    /*public override Enemy_Attack_Base getClone()
    {
        Enemy_Attack_None temp = CreateInstance<Enemy_Attack_None>();
        return temp;
    }*/
}
