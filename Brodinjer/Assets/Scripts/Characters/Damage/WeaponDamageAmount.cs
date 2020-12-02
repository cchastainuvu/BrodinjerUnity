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

    private void Start()
    {
        hit = false;
    }
}
