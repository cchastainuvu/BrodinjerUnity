using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random=UnityEngine.Random;

[RequireComponent((typeof(NavMeshAgent)))]
public class Wander : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;

    public Animator chickenAnim;
    private Vector3 newPos;


    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Walk());
        StartCoroutine(AnimationUpdate());
    }

    private IEnumerator Walk()
    {
        while (true) {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            yield return new WaitForSeconds(wanderTimer);
        }
    }

    private IEnumerator AnimationUpdate()
    {
        if (chickenAnim != null)
        {
            while (true)
            {
                chickenAnim.SetFloat("Speed", agent.velocity.magnitude);
                if (agent.velocity.magnitude <= .01f)
                {
                    chickenAnim.speed = 1;
                }
                else
                {
                    chickenAnim.speed = agent.velocity.magnitude;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        yield return new WaitForFixedUpdate();
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}