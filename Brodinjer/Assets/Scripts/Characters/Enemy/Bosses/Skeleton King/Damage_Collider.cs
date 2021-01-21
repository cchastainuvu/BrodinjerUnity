using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Collider : MonoBehaviour
{
    public Boss_Character_Manager bossManager;
    public LayerMask DamageLayer;
    public Animator anim;
    public string DamageAnimationTrigger;
    public float ArmorAmount;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == ToLayer(DamageLayer.value))
        {
            WeaponDamageAmount temp = coll.GetComponent<WeaponDamageAmount>();
            if (temp != null)
            {
                if (!temp.SingleHit || (temp.SingleHit && !temp.hit))
                {
                    anim.SetTrigger(DamageAnimationTrigger);
                    temp.hit = true;
                    bossManager.TakeDamage(temp.DamageAmount, temp.DecreasedbyArmor, ArmorAmount);
                }
            }
        }
    }
    
    public int ToLayer (int bitmask ) {
        int result = bitmask>0 ? 0 : 31;
        while( bitmask>1 ) {
            bitmask = bitmask>>1;
            result++;
        }
        return result;
    }
}
