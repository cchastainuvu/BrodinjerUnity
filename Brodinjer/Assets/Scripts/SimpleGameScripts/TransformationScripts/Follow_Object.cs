using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Object : MonoBehaviour
{
    public Transform FollowObj;
    public Vector3 offset;
    private bool following;
    public bool OnAwake = true;
    public float speed;

    private void Start()
    {
        if (OnAwake)
        {
            StartFollow();
        }
    }

    public void StartFollow()
    {
        offset = transform.position - FollowObj.position;
        following = true;
        StartCoroutine(Following());
    }

    private IEnumerator Following()
    {
        while (following)
        {
            transform.position = Vector3.Lerp(transform.position, offset + FollowObj.position, Time.deltaTime*speed);
            //transform.position = offset + FollowObj.position;
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopFollow()
    {
        following = false;
    }
}
