using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Idle : NavMesh_Enemy_Base
{
    public override IEnumerator Move()
    {
        yield return null;
    }

    /*public override Enemy_Movement GetClone()
    {
        Enemy_Idle temp = CreateInstance<Enemy_Idle>();
        temp.Speed = 0;
        temp.AngularSpeed = 0;
        return temp;
    }*/
}
