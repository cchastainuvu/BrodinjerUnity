using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class KnockBack_NavMesh : MonoBehaviour
{
    public float thrust; //force
    public float knockTime; //time
    private Vector3 difference;
    private Rigidbody enemyRB;
    private Enemy_Manager enemyManager;
    public Transform BaseObj;
    private Timed_Event Reset;
    private NavMeshAgent agent;
    public UnityEvent OnKnockback, OnKnockbackEnd;

    private void Start()
    {
        if (BaseObj == null)
        {
            BaseObj = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyRB = other.GetComponent<Rigidbody>();
        if (enemyRB != null)
        {
            Debug.Log("Knockback");
            OnKnockback.Invoke();
            enemyManager = other.GetComponent<Enemy_Manager>();

            agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            difference = enemyRB.transform.position - BaseObj.transform.position;
            difference.y = 0;
            difference = difference.normalized * thrust;
            enemyRB.AddForce(difference, ForceMode.Impulse);
            if ((Reset = other.GetComponent<Timed_Event>()) != null)
            {
                Reset.Call();
            }
            
            StartCoroutine(KnockCo(enemyRB, agent, enemyManager));
        }

    }

    private IEnumerator KnockCo(Rigidbody enemy, NavMeshAgent Agent = null, Enemy_Manager Manager = null)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector3.zero;
            if (Agent)
            {
                Agent.enabled = true;
            }
            OnKnockbackEnd.Invoke();
            
            yield return new WaitForSeconds(knockTime);
            
            
        }
    }

}
