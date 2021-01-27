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
    public bool x=true, y=true, z=true;
    private Vector3 newPos;

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
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime*speed);
            //transform.position = offset + FollowObj.position;
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopFollow()
    {
        following = false;
    }
}
