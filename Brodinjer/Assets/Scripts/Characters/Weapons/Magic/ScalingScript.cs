using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingScript : WeaponBase
{
    private WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    private WaitUntil _waitforbutton;
    public GameObject MagicPrefab;
    public Transform InitPos;
    private Rigidbody SpellBall;
    private float currPower;
    public float MaxPower, PowerIncreaseScale, ScaleIncreaseAmount;
    private GameObject currSpell;
    private Vector3 direction, finalScale, increaseScale, newScale;
    private Vector3 rotDirection, initRotation;
    public LimitFloatData MagicAmount;
    public BoolData MagicInUse;
    public float decreaseSpeed;
    public GameObject MagicObj;
    public CameraRotationManager cameraRotation;
    public CameraRotationBase bowCamera;
    public CameraRotationBase thirdPersonCamera;
    public PlayerMovement playermove;
    public CharacterRotate bowRotate;
    private CharacterRotate originalRotate;
    public float maxSpellDuration;
    private float currentSpellDuration;
    public float CameraSwapTime;
    public bool freezeWhenAim;
    public CharacterTranslate freezePlayer;
    private CharacterTranslate originalTranslate;

    public Object_Aim_Script AimScript;

    private Coroutine swapFunc;
    private float currentTime;
    
    
    public override void Initialize()
    {
        //any init stuff needed
        MagicObj.SetActive(true);
        initRotation = transform.rotation.eulerAngles;
        finalScale = MagicPrefab.transform.localScale;
        increaseScale = new Vector3(ScaleIncreaseAmount, ScaleIncreaseAmount, ScaleIncreaseAmount);
        _waitforbutton = new WaitUntil(CheckInput);
        currWeapon = true;
        attack = Attack();
        MagicInUse.value = false;
        originalRotate = playermove.rotate;
        originalTranslate = playermove.translate;
        weaponFunc = StartCoroutine(Attack());

    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
        {
            if (!MagicInUse.value)
            {
                while (frozen)
                {
                    yield return new WaitForFixedUpdate();
                }

                rotDirection = initRotation;
                rotDirection.y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(rotDirection);
                yield return _waitforbutton;
                if (!frozen)
                {
                    inUse = true;
                    MagicInUse.value = true;
                    if (currWeapon && MagicAmount.value > 0 && !frozen)
                    {
                        currPower = 0;
                        currSpell = Instantiate(MagicPrefab, InitPos);
                        currSpell.transform.localScale = Vector3.zero;
                        currSpell.SetActive(true);
                        SpellBall = currSpell.GetComponent<Rigidbody>();
                        while (Input.GetButton(useButton) && MagicAmount.value > 0)
                        {
                            if (cameraRotation.cameraRotation != bowCamera)
                            {
                                if(freezeWhenAim)
                                    playermove.SwapMovement(bowRotate, freezePlayer, playermove.extraControls);
                                else
                                    playermove.SwapMovement(bowRotate, playermove.translate, playermove.extraControls);
                                StartTimeSwap(CameraSwapTime);
                            }

                            cameraRotation.StartTimeSwap(CameraSwapTime, thirdPersonCamera, bowCamera);
                            AimScript.StartAim();
                            //Debug.Log("Current Power: " + currPower);
                            while (frozen)
                            {
                                yield return new WaitForFixedUpdate();
                            }

                            if (currPower >= MaxPower)
                            {
                                currPower = MaxPower;
                            }
                            else
                            {
                                currPower += Time.deltaTime * PowerIncreaseScale;
                                MagicAmount.SubFloat(decreaseSpeed * Time.deltaTime);
                            }

                            if (currSpell.transform.localScale.x <= finalScale.x)
                            {
                                newScale = currSpell.transform.localScale + increaseScale * Time.deltaTime;
                                currSpell.transform.localScale = newScale;
                            }

                            yield return _fixedUpdate;
                        }

                        while (frozen)
                        {
                            yield return new WaitForFixedUpdate();
                        }
                        if(freezeWhenAim)
                            playermove.SwapMovement(bowRotate, originalTranslate, playermove.extraControls);

                        SpellBall.constraints = RigidbodyConstraints.None;
                        currSpell.transform.parent = null;
                        ScalingMagic temp = currSpell.GetComponent<ScalingMagic>();
                        if(temp && temp.VFX)
                            currSpell.GetComponent<ScalingMagic>().VFX.SetActive(true);
                        SpellBall.AddForce(transform.forward * currPower, ForceMode.Impulse);
                        currentSpellDuration = maxSpellDuration * (currPower / MaxPower);
                        while (currentSpellDuration > 0 && inUse && MagicInUse.value)
                        {
                            currentSpellDuration -= Time.deltaTime;
                            yield return _fixedUpdate;
                        }

                        if (currSpell == null || !currSpell.GetComponent<ScalingMagic>().hitObj)
                        {
                            inUse = false;
                            MagicInUse.value = false;
                            Destroy(currSpell);
                        }

                        inUse = false;

                        AimScript.StopAim();

                    }
                }
            }

            yield return _fixedUpdate;

        }
    }
    

    public override void End()
    {
        //and end stuff needed
        MagicObj.SetActive(false);
        inUse = false;
        currWeapon = false;
        if (cameraRotation.cameraRotation != thirdPersonCamera)
        {
            cameraRotation.StopTimeSwap(thirdPersonCamera);
            playermove.SwapMovement(originalRotate, originalTranslate, playermove.extraControls);
        }

        if (weaponFunc != null)
        {
            StopCoroutine(weaponFunc);
        }
    }

    public void SpellHit()
    {
        currentSpellDuration = 0;
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
    
    public void StartTimeSwap(float time)
    {
        if (swapFunc == null)
        {
            currentTime = time;
            swapFunc = StartCoroutine(TimedSwap());
        }
        else
        {
            currentTime = time;
        }
        
    }

    private IEnumerator TimedSwap()
    {
        while (currentTime > 0)
        {
            currentTime -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        StopTimeSwap();
    }

    public void StopTimeSwap()
    {
        if (swapFunc != null)
        {
            playermove.SwapMovement(originalRotate, originalTranslate, playermove.extraControls);
            StopCoroutine(swapFunc);
        }
        else if(originalRotate != playermove.rotate)
        {
            playermove.SwapMovement(originalRotate, originalTranslate, playermove.extraControls);
        }
        swapFunc = null;

    }
}
