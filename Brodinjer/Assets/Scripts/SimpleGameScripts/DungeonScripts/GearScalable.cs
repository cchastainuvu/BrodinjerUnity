using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearScalable : ScalableObject
{
    public bool canScale;
    
    public override bool ScaleUp(bool deltaTimed)
    {
        if (canScale)
        {
            return base.ScaleUp(deltaTimed);
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canScale)
        {
            if (other.CompareTag("Scalable") || other.CompareTag("GearStop"))
            {
                canScale = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canScale)
        {
            if (other.CompareTag("Scalable") || other.CompareTag("GearStop"))
            {
                canScale = true;
            }
        }
    }
}
