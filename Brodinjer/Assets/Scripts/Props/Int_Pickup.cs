using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Objects/Pickup/Int")]
public class Int_Pickup : PickUp_Base
{
    public IntData intdata;
    public int addAmount;
    
    public override void Pickup()
    {
        intdata.AddInt(addAmount);
    }
}
