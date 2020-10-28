using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PS_Callback : MonoBehaviour
{
    public UnityEvent onPSStopped;
    
    public void OnParticleSystemStopped()
    {
        onPSStopped.Invoke();
    }
}
