using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    //Put this script on the other object (not the one that will actually get knock back)
    public float thrust; //force
    public float knockTime; //time
    private Vector3 difference;
    private Rigidbody enemyRB;
    


    private void OnTriggerEnter(Collider other)
    {
            enemyRB = other.GetComponent<Rigidbody>();
        if (enemyRB != null)
        {
            difference = enemyRB.transform.position - transform.position;
            difference = difference.normalized * thrust;
            enemyRB.AddForce(difference, ForceMode.Impulse);
            StartCoroutine(KnockCo(enemyRB));
        }

    }

    private IEnumerator KnockCo(Rigidbody enemy)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector3.zero;
        }
    }

}
