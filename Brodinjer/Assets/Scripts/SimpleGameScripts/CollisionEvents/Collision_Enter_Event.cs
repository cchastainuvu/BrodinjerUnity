using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Enter_Event : Collision_Event_Base
{
    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(CheckCollision(other));
    }
}
