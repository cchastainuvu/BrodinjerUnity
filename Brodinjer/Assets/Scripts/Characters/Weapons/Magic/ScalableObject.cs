using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;

public class ScalableObject : MonoBehaviour
{
    public float MinScaleMultiplier, MaxScaleMultiplier;
    public GameObject HighlightObj;
    [HideInInspector] public Vector3 minScale, maxScale;

    private Vector3 newScale;

    public bool UpdateMass;
    private Rigidbody rigid;
    public float MinMassMultiplier, MaxMassMultiplier;
    private float newMass;
    private float minMass, maxMass;

    public float ScaleSpeed;
    private float scaleSpeed;

    private float currentScaleAmount;

    public FixedJoint joint;
    public float minJointMassMultiplier, maxJointMassMultiplier;
    private float minJointMass, maxJointMass;

    public bool pulleySystem;
    public ConfigurableJoint configurableJoint;
    public float linearMinLimit, linerMaxLimit;
    private float currentLimit;

    private float initScaleAmount;
    
    private void Start()
    {
        minScale = transform.localScale * MinScaleMultiplier;
        maxScale = transform.localScale * MaxScaleMultiplier;
        if (UpdateMass)
        {
            rigid = GetComponent<Rigidbody>();
            if (!rigid)
            {
                rigid = gameObject.AddComponent<Rigidbody>();
            }

            minMass = rigid.mass * MinMassMultiplier;
            maxMass = rigid.mass * MaxMassMultiplier;
            if (joint)
            {

                minJointMass = joint.massScale * minJointMassMultiplier;
                maxJointMass = joint.massScale * maxJointMassMultiplier;
            }

            if (pulleySystem)
            {
                initScaleAmount = transform.localScale.x;
                currentLimit = linearMinLimit;
                SoftJointLimit temp = new SoftJointLimit {limit = linearMinLimit};
                configurableJoint.linearLimit = temp;
            }
        }
    }

    public void ScaleUp(bool deltaTimed)
    {
        scaleSpeed = (deltaTimed) ? ScaleSpeed * Time.deltaTime : ScaleSpeed;
        if (transform.localScale.x < maxScale.x)
        {
            newScale = transform.localScale;
            newScale += scaleSpeed * Vector3.one;
            if (newScale.x > maxScale.x)
            {
                newScale = maxScale;
            }
            transform.localScale = newScale;
            if (UpdateMass)
            {
                rigid.mass = Mathf.Lerp(minMass, maxMass,
                    GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                if (joint)
                {
                    joint.massScale = Mathf.Lerp(maxJointMass, minJointMass,
                        GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                }
            }

            if (pulleySystem)
            {
                currentLimit = Mathf.Lerp(linearMinLimit, linerMaxLimit,
                    GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                SoftJointLimit temp = new SoftJointLimit{limit = currentLimit};
                configurableJoint.linearLimit = temp;
            }
        }
    }

    public void ScaleDown(bool deltaTimed)
    {
        scaleSpeed = (deltaTimed) ? ScaleSpeed * Time.deltaTime : ScaleSpeed;
        if (transform.localScale.x > minScale.x)
        {
            newScale = transform.localScale;
            newScale -= scaleSpeed * Vector3.one;
            if (newScale.x < minScale.x)
            {
                newScale = minScale;
            }
            transform.localScale = newScale;
            if (UpdateMass)
            {
                rigid.mass = Mathf.Lerp(minMass, maxMass,
                    GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                if (joint)
                {
                    joint.massScale = Mathf.Lerp(maxJointMass, minJointMass,
                        GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                }
            }
            if (pulleySystem)
            {
                currentLimit = Mathf.Lerp(linearMinLimit, linerMaxLimit,
                    GeneralFunctions.ConvertRange(minScale.x, maxScale.x, 0, 1, newScale.x));
                SoftJointLimit temp = new SoftJointLimit{limit = currentLimit};
                configurableJoint.linearLimit = temp;
            }
        }
    }
    
}
