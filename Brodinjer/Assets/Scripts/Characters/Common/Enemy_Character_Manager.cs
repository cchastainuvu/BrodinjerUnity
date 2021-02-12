using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy_Character_Manager : Character_Manager
{
    public Enemy_Manager enemyManager;
    private Vector3 difference;
    private Rigidbody enemyRB;
    private Timed_Event Reset;
    private bool running;
    private float currentTime;

    public override void Init()
    {
        base.Init();
        if(agent == null)
            agent = enemyManager.GetComponent<NavMeshAgent>();
        running = false;

    }

    public override IEnumerator Stun(float stuntime)
    {
        enemyManager.Stun();
        yield return new WaitForSeconds(stuntime);
        enemyManager.UnStun();
        stunned = false;
    }
    
    protected override void StartKnockback(WeaponDamageAmount other)
    {
        if (agent != null)
        {
            agent.enabled = false;
            difference = /*agent.transform.position + */(other.BaseObj.transform.forward*other.KnockbackForce);
            if (!running)
            {
                StartCoroutine(KnockCo(other));
            }
        }

    }

    protected virtual IEnumerator KnockCo(WeaponDamageAmount weapon)
    {
        running = true;
        if (agent != null)
        {
            currentTime = weapon.KnockbackTime;
            while (currentTime > 0)
            {
                agent.transform.Translate(difference * Time.deltaTime);
                currentTime -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        agent.enabled = true;
        running = false;
    }

    private void OnDisable()
    {
        running = false;
    }
}
