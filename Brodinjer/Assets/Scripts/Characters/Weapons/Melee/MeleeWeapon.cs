using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class MeleeWeapon : WeaponBase
{
    public GameObject knockbackObj, artObj;
    private WaitUntil waitforbutton;
    public float attackActiveTime01, attackActiveTime02, attackActiveTime03, attackcoolDownTime;
    private float currentTime;
    public UnityEvent AxUsedEvent;
    private WaitForFixedUpdate fixedUpdate;
    public float comboTime01, comboTime02, combo03Cooldown;
    public string ComboNumInteger;
    public float minComboWaitTime01, minComboWaitTime02;
    public float attackActivateTime01, attackActivateTime02, attackActivateTime03;
    public PlayerMovement playermove;
    public bool freezeWhenAttack;
    public CharacterTranslate freezeMovement;
    private CharacterTranslate origMovement;
    public CharacterRotate freezeRotation;
    private CharacterRotate origRotation;
    
    public override void Initialize()
    {
        artObj.SetActive(true);
        knockbackObj.SetActive(false);
        waitforbutton = new WaitUntil(CheckInput);
        fixedUpdate = new WaitForFixedUpdate();
        currWeapon = true;
        weaponFunc = StartCoroutine(Attack());
    }

    public override IEnumerator Attack()
    {
        origMovement = playermove.translate;
        origRotation = playermove.rotate;
        while (currWeapon)
        {
            while (frozen)
            {
                yield return fixedUpdate;
            }
            yield return waitforbutton;
            if(freezeWhenAttack)
                playermove.SwapMovement(freezeRotation, freezeMovement);
            if (!frozen)
            {
                if (currWeapon)
                {
                    anim.ResetTrigger(AttackEndTrigger);
                    anim.SetTrigger(AttackTrigger);
                    anim.SetInteger(ComboNumInteger, 1);
                }

                yield return  new WaitForSeconds(attackActivateTime01);
                knockbackObj.SetActive(true);
                AxUsedEvent.Invoke();
                currentTime = 0;
                while (currentTime < comboTime01 && !frozen && currWeapon)
                {
                    if (currentTime > attackActiveTime03 && knockbackObj.activeSelf)
                    {
                        //knockbackObj.SetActive(false);
                    }
                    if (CheckInput() && currentTime >= minComboWaitTime01)
                    {
                        //knockbackObj.SetActive(false);
                        anim.SetInteger(ComboNumInteger, 2);
                        yield return  new WaitForSeconds(attackActivateTime02);
                        //knockbackObj.SetActive(true);
                        currentTime = 0;
                        while (currentTime < comboTime02 && !frozen && currWeapon)
                        {
                            if (currentTime > attackActiveTime02 && knockbackObj.activeSelf)
                            {
                                //knockbackObj.SetActive(false);
                            }
                            if (CheckInput()&& currentTime >= minComboWaitTime02)
                            {
                                //knockbackObj.SetActive(false);
                                anim.SetInteger(ComboNumInteger, 3);
                                yield return  new WaitForSeconds(attackActivateTime03);
                                //knockbackObj.SetActive(true);
                                currentTime = 0;
                                while (currentTime < attackActiveTime03 && !frozen && currWeapon)
                                {
                                    currentTime += Time.deltaTime;
                                    yield return fixedUpdate;
                                }
                                //knockbackObj.SetActive(false);
                                yield return new WaitForSeconds(combo03Cooldown);
                                currentTime = comboTime02 +comboTime01;
                            }

                            currentTime += Time.deltaTime;
                            yield return fixedUpdate;
                        }
                    }

                    currentTime += Time.deltaTime;
                    yield return fixedUpdate;
                }
                if(freezeWhenAttack)
                    playermove.SwapMovement(origRotation, origMovement);
                Debug.Log("Finish Attack");
                knockbackObj.SetActive(false);
                anim.SetInteger(ComboNumInteger, 0);
                anim.SetTrigger(AttackEndTrigger);
                yield return new WaitForSeconds(attackcoolDownTime);
            }

            yield return fixedUpdate;
        }
    }

    public override void End()
    {
        artObj.SetActive(false);
        knockbackObj.SetActive(false);
        currWeapon = false;
        anim.SetInteger(ComboNumInteger, 0);
        anim.SetTrigger(AttackEndTrigger);
        if(weaponFunc != null)
            StopCoroutine(weaponFunc);
    }
    
    private bool CheckInput()
    {
        if (Input.GetButtonDown(useButton))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
