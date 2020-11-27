using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Obj_Position_Range : Instantiate_Obj
{

    public Transform Corner01, Corner02;

    private Vector3 randomPosition;


    public override void InstantiateObj()
    {
        randomPosition.x = Random.Range(Corner01.position.x, Corner02.position.x);
        randomPosition.y = Random.Range(Corner01.position.y, Corner02.position.y);
        randomPosition.z = Random.Range(Corner01.position.z, Corner02.position.z);
        
        base.InstantiateObj(randomPosition);
    }
}
