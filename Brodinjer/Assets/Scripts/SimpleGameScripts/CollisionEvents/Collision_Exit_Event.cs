using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Exit_Event : Collision_Event_Base
{
    private void OnCollisionExit(Collision other)
    {
        StartCoroutine(CheckCollision(other));
    }
}
