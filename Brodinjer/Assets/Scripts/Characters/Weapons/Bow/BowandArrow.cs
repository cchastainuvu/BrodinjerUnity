using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class BowandArrow : WeaponBase
{
    //hold e to increase power
    //release e to shoot
    //Shoots straight forward from player
    
    private WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    private WaitUntil _waitforbutton;
    public GameObject ArrowPrefab;
    public Transform InitPos;
    private Rigidbody ArrowRB;
    public string useButton;
    private float currPower;
    public float MaxPower, PowerIncreaseScale;
    private GameObject currArrow;
    private Vector3 direction;
    private Vector3 rotDirection, initRotation;
    public GameObject WeaponObj;
    public CameraRotationManager cameraRotation;
    public CameraRotationBase bowCamera;
    public CameraRotationBase thirdPersonCamera;
    public PlayerMovement playermove;
    public CharacterRotate bowRotate;
    private CharacterRotate originalRotate;

    public Object_Aim_Script AimScript;
    public float CameraSwapTime;

    public UnityEvent BowEquiped, BowPulled, ArrowFired, BowUnequipped;
    
    public override void Initialize()
    {
        //any init stuff needed
        BowEquiped.Invoke();
        initRotation = transform.rotation.eulerAngles;
        _waitforbutton = new WaitUntil(CheckInput);
        currWeapon = true;
        WeaponObj.SetActive(true);
        attack = Attack();
        originalRotate = playermove.rotate;
        StartCoroutine(attack);

    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
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
                BowPulled.Invoke();
                inUse = true;
                if (currWeapon)
                {
                    currPower = 0;
                    currArrow = Instantiate(ArrowPrefab, InitPos);
                    currArrow.SetActive(true);
                    ArrowRB = currArrow.GetComponent<Rigidbody>();
                    while (Input.GetButton(useButton))
                    {
                        if (cameraRotation.cameraRotation != bowCamera)
                        {
                            playermove.SwapMovement(bowRotate, playermove.translate, playermove.extraControls);
                        }

                        while (frozen)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        cameraRotation.StartTimeSwap(CameraSwapTime, thirdPersonCamera, bowCamera);
                        AimScript.StartAim();
                        currPower += Time.deltaTime * PowerIncreaseScale;
                        if (currPower >= MaxPower)
                        {
                            currPower = MaxPower;
                        }

                        yield return _fixedUpdate;
                    }

                    while (frozen)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    ArrowFired.Invoke();
                    ArrowRB.constraints = RigidbodyConstraints.None;
                    currArrow.transform.parent = null;
                    ArrowRB.AddForce(transform.forward * currPower, ForceMode.Impulse);
                    inUse = false;
                    AimScript.StopAim();
                }

                yield return new WaitForFixedUpdate();
            }
        }

    }
    

    public override void End()
    {
        //and end stuff needed
        BowUnequipped.Invoke();
        WeaponObj.SetActive(false);
        currWeapon = false;
        inUse = false;
        if (cameraRotation.cameraRotation != thirdPersonCamera)
        {
            cameraRotation.StopTimeSwap(thirdPersonCamera);
            playermove.SwapMovement(originalRotate, playermove.translate);
        }
        StopCoroutine(attack);
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
