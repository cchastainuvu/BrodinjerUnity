using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Enter_Damage : Trigger_Event_Base
{
    public float Damage;
    public bool DecreasedByArmor;
    public bool damaged;
    private GameObject triggerCollider;
    public float DamageCoolDown;

    private void Start()
    {
        isRunning = false;
        damaged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerCollider = other.gameObject;
        if (!damaged && !isRunning)
        {
            StartCoroutine(CheckTrigger(other));
        }
    }

    public override void RunEvent()
    {
        if (active)
        {
            damaged = true;
            StartCoroutine(damageCooldown());
            Character_Manager cm = triggerCollider.GetComponent<Character_Manager>();
            if (cm)
            {
                cm.TakeDamage(Damage, DecreasedByArmor);
                Event.Invoke();
            }
        }
    }

    private IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(DamageCoolDown);
        damaged = false;
        isRunning = false;
    }

    private void OnDisable()
    {
        damaged = false;
        isRunning = false;
    }
}
