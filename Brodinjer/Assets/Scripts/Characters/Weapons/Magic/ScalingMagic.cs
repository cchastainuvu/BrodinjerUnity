using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ScalingMagic : MonoBehaviour
{
    //Add Local scale point
    public PlayerMovement movement;
    public WeaponManager wm;
    private Transform ScalingObj;
    public float ScaleTime;
    private float timeLeft;
    private WaitForFixedUpdate _fixedUpdate;
    private ScalableObject scaleObj;
    private Vector3 newScale, scaleIncrease;
    public float IncreaseAmount;
    public string stopButton;
    [HideInInspector] public bool hitObj = false;
    public GameObject art;
    public LimitFloatData MagicAmount;
    public BoolData MagicInUse;
    public float decreaseSpeed;
    public ScalingScript scalescript;
    public GameObject VFX;
    public string ScaleAxis;

    private void Start()
    {
        hitObj = false;
        _fixedUpdate = new WaitForFixedUpdate();
        scaleIncrease = new Vector3(IncreaseAmount, IncreaseAmount, IncreaseAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitObj)
        {
            if (other.CompareTag("Scalable"))
            {
                hitObj = true;
                ScalingObj = other.gameObject.transform;
                scaleObj = ScalingObj.GetComponent<ScalableObject>();
                art.SetActive(false);
                StartCoroutine(Scale());
            }
        }

    }

    private IEnumerator Scale()
    {
        //movement.StopAll();
        timeLeft = ScaleTime;
        scaleObj.HighlightObj.SetActive(true);
        scalescript.inUse = true;
        while (MagicAmount.value > 0 && timeLeft > 0 && scalescript.currWeapon)
        {
            newScale = ScalingObj.localScale;
            if (Input.GetAxis(ScaleAxis) > 0)
            {
                MagicAmount.SubFloat(decreaseSpeed*Time.deltaTime);
                scaleObj.ScaleUp(true);
            }
            else if (Input.GetAxis(ScaleAxis) < 0)
            {
                MagicAmount.SubFloat(decreaseSpeed*Time.deltaTime);
                scaleObj.ScaleDown(true);
            }
            if (Input.GetButtonDown(stopButton))
            {
                timeLeft = 0;
            }
            timeLeft -= Time.deltaTime;
            yield return _fixedUpdate;
        }
        MagicInUse.value = false;
        //scalescript.inUse = false;
        scaleObj.HighlightObj.SetActive(false);
        //movement.StartAll();
        yield return new WaitForSeconds(.1f);
        Destroy(this);
    }
    
    
}
