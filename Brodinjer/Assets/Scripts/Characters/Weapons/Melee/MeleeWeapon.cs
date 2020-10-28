using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class MeleeWeapon : WeaponBase
{
    public GameObject knockbackObj, artObj;
    public string useButton;
    private WaitUntil waitforbutton;
    public float attackActiveTime, attackcoolDownTime;
    private float currentTime;
    public UnityEvent AxUsedEvent;
    
    public override void Initialize()
    {
        artObj.SetActive(true);
        knockbackObj.SetActive(false);
        waitforbutton = new WaitUntil(CheckInput);
        currWeapon = true;
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
        {
            yield return waitforbutton;
            if (!frozen)
            {
                knockbackObj.SetActive(true);
                AxUsedEvent.Invoke();
                currentTime = attackActiveTime;
                while (currentTime > 0 && !frozen)
                {
                    currentTime -= Time.deltaTime;
                    yield return new WaitForFixedUpdate();
                }
                knockbackObj.SetActive(false);
                while (frozen)
                {
                    yield return new WaitForFixedUpdate();
                }

                yield return new WaitForSeconds(attackcoolDownTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public override void End()
    {
        artObj.SetActive(false);
        knockbackObj.SetActive(false);
    }
    
    private bool CheckInput()
    {
        if (Input.GetButtonDown(useButton) || !currWeapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
