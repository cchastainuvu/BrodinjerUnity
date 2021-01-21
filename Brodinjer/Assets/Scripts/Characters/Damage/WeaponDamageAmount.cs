using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageAmount : MonoBehaviour
{
    public float DamageAmount;
    public bool DecreasedbyArmor;

    public float StunTime;

    public bool SingleHit = false;
    [HideInInspector]
    public bool hit = false;

    public float KnockbackForce, KnockbackTime;

    public GameObject BaseObj;

    private void Start()
    {
        if(BaseObj == null)
        {
            BaseObj = this.gameObject;
        }
        hit = false;
    }
}
