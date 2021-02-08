using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRigidbodies : MonoBehaviour
{
    private Rigidbody[] rigidbodies;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Activate()
    {
        foreach (var rigid in rigidbodies)
        {
            rigid.isKinematic = false;
            rigid.useGravity = true;
            rigid.GetComponent<Collider>().enabled = true;
        }
    }

    public void Deactivate()
    {
        foreach(var rigid in rigidbodies)
        {
            rigid.isKinematic = true;
            rigid.useGravity = false;
        }
    }

    public void RemoveConstraints()
    {
        foreach(var rigid in rigidbodies)
        {
            rigid.constraints = RigidbodyConstraints.None;
        }
    }
    
}
