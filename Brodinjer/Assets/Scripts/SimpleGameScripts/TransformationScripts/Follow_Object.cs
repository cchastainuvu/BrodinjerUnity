﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Object : MonoBehaviour
{
    public Transform FollowObj;
    public Vector3 offset;
    private bool following;
    public bool OnAwake = true;
    public float speed = -1;
    public bool x=true, y=true, z=true;
    private Vector3 newPos;
    private bool started = false;

    private void Start()
    {
        following = false;
        if (OnAwake)
        {
            StartFollow();
        }
    }

    public void StartFollow()
    {
        if (!following)
        {
            following = true;
            offset = transform.position - FollowObj.position;
            StartCoroutine(Following());
        }
    }

    private IEnumerator Following()
    {
        while (following)
        {
            newPos = offset + FollowObj.position;
            if (!x)
                newPos.x = transform.position.x;
            if (!y)
                newPos.y = transform.position.y;
            if (!z)
                newPos.z = transform.position.z;
            if (speed < 0)
            {
                transform.position = offset + FollowObj.position;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
            }
           
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopFollow()
    {
        following = false;
    }
}
