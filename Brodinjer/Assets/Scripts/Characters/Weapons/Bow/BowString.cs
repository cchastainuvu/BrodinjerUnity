using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
    public float speed;
    public bool followHand;
    public Transform HandPosition, BowPosition;
    public float pullWaitTime, releaseWaittime;

    private void FixedUpdate()
    {
        if (followHand)
        {
            transform.position = HandPosition.transform.position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, BowPosition.position, Time.deltaTime*speed);
        }
    }

    public void Pull()
    {
        StartCoroutine(PullWait());
    }

    private IEnumerator PullWait()
    {
        yield return new WaitForSeconds(pullWaitTime);
        followHand = true;
    }

    public void Release()
    {
        StartCoroutine(ReleaseWait());
    }

    private IEnumerator ReleaseWait()
    {
        yield return new WaitForSeconds(releaseWaittime);
        followHand = false;
    }
}
