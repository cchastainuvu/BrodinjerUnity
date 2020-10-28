using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Stay_Event : Collision_Event_Base
{
    private void OnCollisionStay(Collision other)
    {
        StartCoroutine(CheckCollision(other));
    }
}
