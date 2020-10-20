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

    private void Start()
    {
        if (OnAwake)
        {
            StartRotate();
        }
    }

    public void StartRotate()
    {
        Debug.Log(name + ": " + FollowRotateObject);
        rotating = true;
        rotationOffset = FollowRotateObject.eulerAngles - transform.eulerAngles;
        rotateFunc = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (rotating)
        {
            //transform.rotation = FollowRotateObject.rotation;
            transform.eulerAngles = FollowRotateObject.eulerAngles + rotationOffset;
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
