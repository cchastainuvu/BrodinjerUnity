using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chickenwalk : MonoBehaviour
{
    //public Animator anim;
    public NavMeshAgent agent;

    public void Update()
    {
        if (agent.velocity.magnitude <= 1)
        {
            print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        } 
    }
}
