using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Rotate : MonoBehaviour
{
    public Transform FollowRotateObject;
    private Coroutine rotateFunc;
    private bool rotating;
    public bool OnAwake;
    private Vector3 rotationOffset;
    public bool x = true, y = true, z = true;
    public bool UseOffset = true;
    private bool started = false;
    private void Start()
    {
        if (OnAwake)
        {
            StartRotate();
        }
    }

    public void StartRotate()
    {
        rotating = true;
        if(UseOffset)
            rotationOffset = FollowRotateObject.eulerAngles - transform.eulerAngles;
        else
            rotationOffset = Vector3.zero;
        rotateFunc = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (rotating)
        {
            //transform.rotation = FollowRotateObject.rotation;
            Vector3 eulerAngleNew = transform.eulerAngles;
            if (x)
                eulerAngleNew.x = FollowRotateObject.eulerAngles.x + rotationOffset.x;
            if(y)
                eulerAngleNew.y = FollowRotateObject.eulerAngles.y + rotationOffset.y;
            if(z)
                eulerAngleNew.z = FollowRotateObject.eulerAngles.z + rotationOffset.z;
            transform.eulerAngles = eulerAngleNew;
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopRotate()
    {
        rotating = false;
        if(rotateFunc != null)
            StopCoroutine(Rotate());
    }
    
    
}
