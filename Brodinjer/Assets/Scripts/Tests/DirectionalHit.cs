using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalHit : Trigger_Event_Base
{
    public Transform player;
    public Animator anim;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CheckTrigger(other));
    }

    public override IEnumerator CheckTrigger(Collider coll)
    {
        switch (checksFor)
        {
            case Check.Layer:
                
                if (layer == (layer | (1 << coll.gameObject.layer)))
                {
                    Debug.Log("Layer Correct");
                    yield return new WaitForSeconds(waitTime);
                    anim.SetFloat("Direction", GetDirection());
                    anim.SetTrigger("Hit");
                }
                break;
            case Check.Name:
                if (coll.gameObject.name.Contains(objName))
                {
                    yield return new WaitForSeconds(waitTime);
                    anim.SetFloat("Direction", GetDirection());
                    anim.SetTrigger("Hit");
                }
                break;
            case Check.Tag:
                if (coll.CompareTag(tagName))
                {
                    yield return new WaitForSeconds(waitTime);
                    anim.SetFloat("Direction", GetDirection());
                    anim.SetTrigger("Hit");
                }
                break;
        }
    }

    public virtual float GetDirection()
    {
        Debug.Log(player.position);
        Vector3 collisionposition = player.position;
        collisionposition.y = 0;
        Vector3 transformposition = transform.position;
        transformposition.y = 0;
        Vector3 target = collisionposition - transformposition;
        float angle = Vector3.Angle(target, transform.forward);
        Vector3 crossProduct = Vector3.Cross(target, transform.forward);
        if (crossProduct.y < 0)
        {
            angle = -angle;
        }

        angle /= 360;
        angle += .5f;
        Debug.Log(angle);
        return angle;
    }
}
