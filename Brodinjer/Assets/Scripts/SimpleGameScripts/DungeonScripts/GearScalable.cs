using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearScalable : ScalableObject
{
    public bool canScale;
    
    public override void ScaleUp(bool deltaTimed)
    {
        if (canScale)
        {
            base.ScaleUp(deltaTimed);
        }
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
