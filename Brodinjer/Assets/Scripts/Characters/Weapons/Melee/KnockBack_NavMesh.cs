using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockBack_NavMesh : MonoBehaviour
{
    public float thrust; //force
    public float knockTime; //time
    private Vector3 difference;
    private Rigidbody enemyRB;
    private NavMesh_Enemy_Base enemyManager;
    public Transform BaseObj;
    private Timed_Event Reset;

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
            enemyManager = other.GetComponent<NavMesh_Enemy_Base>();
            if (enemyManager != null)
            {
                enemyManager.agent.enabled = false;
            }
            difference = enemyRB.transform.position - BaseObj.position;
            difference.y = 0;
            difference = difference.normalized * thrust;
            enemyRB.AddForce(difference, ForceMode.Impulse);
            if ((Reset = other.GetComponent<Timed_Event>()) != null)
            {
                Reset.Call();
            }
            StartCoroutine(KnockCo(enemyRB, enemyManager));
        }

    }

    private IEnumerator KnockCo(Rigidbody enemy, NavMesh_Enemy_Base Manager = null)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector3.zero;
            if (Manager)
            {
                Manager.agent.enabled = true;
            }
        }
    }

}
