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
    private WaitForFixedUpdate fixedUpdate;
    public float comboTime;
    
    public override void Initialize()
    {
        artObj.SetActive(true);
        knockbackObj.SetActive(false);
        waitforbutton = new WaitUntil(CheckInput);
        fixedUpdate = new WaitForFixedUpdate();
        currWeapon = true;
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
        {
            while (frozen)
            {
                yield return fixedUpdate;
            }
            yield return waitforbutton;
            if (!frozen)
            {
                knockbackObj.SetActive(true);
                AxUsedEvent.Invoke();
                currentTime = attackActiveTime;
                Debug.Log("Hit 1");
                while (currentTime > 0 && !frozen)
                {
                    currentTime -= Time.deltaTime;
                    yield return fixedUpdate;
                }
                knockbackObj.SetActive(false);
                currentTime = 0;
                while (currentTime < comboTime && !frozen)
                {
                    if (CheckInput())
                    {
                        Debug.Log("Hit 2");
                        knockbackObj.SetActive(true);
                        currentTime = 0;
                        while (currentTime < attackActiveTime && !frozen)
                        {
                            currentTime += Time.deltaTime;
                            yield return fixedUpdate;
                        }

                        knockbackObj.SetActive(false);
                        currentTime = 0;
                        while (currentTime < comboTime && !frozen)
                        {
                            if (CheckInput())
                            {
                                Debug.Log("Hit 3");
                                knockbackObj.SetActive(true);
                                currentTime = 0;
                                while (currentTime < attackActiveTime && !frozen)
                                {
                                    currentTime += Time.deltaTime;
                                    yield return fixedUpdate;
                                }

                                knockbackObj.SetActive(false);
                                currentTime = comboTime;
                            }

                            currentTime += Time.deltaTime;
                            yield return fixedUpdate;
                        }
                    }

                    currentTime += Time.deltaTime;
                    yield return fixedUpdate;
                }
                yield return new WaitForSeconds(attackcoolDownTime);
            }

            yield return fixedUpdate;
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
