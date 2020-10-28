using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Objects/Pickup/Float")]
public class Float_Pickup : PickUp_Base
{
    public FloatData floatdata;
    public float addAmount;
    
    public override void Pickup()
    {
        floatdata.AddFloat(addAmount);
    }
}
