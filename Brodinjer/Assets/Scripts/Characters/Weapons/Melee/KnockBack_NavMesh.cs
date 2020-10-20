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
    private Enemy_Manager enemyManager;
    public Transform BaseObj;

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
            enemyManager = other.GetComponent<Enemy_Manager>();
            if (enemyManager != null)
            {
                enemyManager.agent.enabled = false;
            }
            difference = enemyRB.transform.position - BaseObj.position;
            difference.y = 0;
            difference = difference.normalized * thrust;
            enemyRB.AddForce(difference, ForceMode.Impulse);
            StartCoroutine(KnockCo(enemyRB, enemyManager));
        }

    }

    private IEnumerator KnockCo(Rigidbody enemy, Enemy_Manager Manager = null)
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
