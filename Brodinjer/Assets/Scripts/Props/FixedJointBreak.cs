using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FixedJointBreak : MonoBehaviour
{
    public UnityEvent onJointBroken;

    private void OnJointBreak(float breakForce)
    {
        onJointBroken.Invoke();
    }
}
