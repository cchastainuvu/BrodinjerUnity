using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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
    public CameraSwitch currentCam;
    public CameraBase bowCamera;
    public CameraBase thirdPersonCamera;
    public PlayerMovement playermove;
    public CharacterRotate bowRotate;
    private CharacterRotate originalRotate;

    public Object_Aim_Script AimScript;
    public float CameraSwapTime;
    
    public override void Initialize()
    {
        //any init stuff needed
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
            rotDirection = initRotation;
            rotDirection.y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(rotDirection);
            yield return _waitforbutton;
            inUse = true;
            if (currWeapon)
            {
                currPower = 0;
                currArrow = Instantiate(ArrowPrefab, InitPos);
                currArrow.SetActive(true);
                ArrowRB = currArrow.GetComponent<Rigidbody>();
                while (Input.GetButton(useButton))
                {
                    if (currentCam.cameraScript != bowCamera)
                    {
                        playermove.SwapMovement(bowRotate, playermove.translate, playermove.extraControls);
                    }
                    currentCam.StartTimeSwap(CameraSwapTime, thirdPersonCamera, bowCamera);
                    AimScript.StartAim();
                    currPower += Time.deltaTime * PowerIncreaseScale;
                    if (currPower >= MaxPower)
                    {
                        currPower = MaxPower;
                    }

                    yield return _fixedUpdate;
                }
                ArrowRB.constraints = RigidbodyConstraints.None;
                currArrow.transform.parent = null;
                ArrowRB.AddForce(transform.forward * currPower, ForceMode.Impulse);
                inUse = false;
                AimScript.StopAim();
            }

        }

    }
    

    public override void End()
    {
        //and end stuff needed
        WeaponObj.SetActive(false);
        currWeapon = false;
        inUse = false;
        if (currentCam.cameraScript != thirdPersonCamera)
        {
            currentCam.StopTimeSwap(thirdPersonCamera);
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
