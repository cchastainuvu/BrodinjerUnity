using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Character_Manager : MonoBehaviour
{
    public Character_Base Character;
    private Character_Base _characterTemp;
    public bool MainCharacter = false;
    
    public LayerMask DamageLayer;
    public float damageCoolDown;

    public bool StunTime;

    protected bool damaged, stunned, dead;

    public Animation_Base DamageAnimation;
    public Animator anim;
    public NavMeshAgent agent;
    private Transform player;


    private void Start()
    {
        damaged = false;
        stunned = false;
        dead = false;
        player = FindObjectOfType<PlayerMovement>().transform;
        if(DamageAnimation)
            DamageAnimation.Init(this, anim, player, agent);
        Init();
    }

    public virtual void Init()
    {
        if (!MainCharacter)
        {
            _characterTemp = Character.getClone();
            Character = _characterTemp;
        }
        Character.Init(this, transform, MainCharacter);
        if (GetComponent<Death_Event_Setup>() != null && Character.Health.Death_Version is Death_Event)
        {
            Death_Event death = Character.Health.Death_Version as Death_Event;
            if (death != null)
            {
                death._event = GetComponent<Death_Event_Setup>().DeathEvent;
            }
        }
    }

    public virtual void TakeDamage(float amount, bool armor)
    {
        if (!dead)
        {
            Character.Health.TakeDamage(amount, armor);
            if (Character.Health.health.value <= 0)
            {
                return;
            }
        }
    }

    private IEnumerator OnTriggerEnter(Collider coll)
    {
        if (!damaged && !dead)
        {
            if (coll.gameObject.layer == ToLayer(DamageLayer.value))
            {
                WeaponDamageAmount temp = coll.GetComponent<WeaponDamageAmount>();
                if (temp != null)
                {
                    if (StunTime && !stunned)
                    {
                        stunned = true;
                        StartCoroutine(Stun(temp.StunTime));
                    }

                    damaged = true;
                    if(DamageAnimation)
                        DamageAnimation.StartAnimation();
                    if (!temp.SingleHit || (temp.SingleHit && !temp.hit))
                    {
                        temp.hit = true;
                        TakeDamage(temp.DamageAmount, temp.DecreasedbyArmor);
                    }

                    yield return new WaitForSeconds(damageCoolDown);
                    damaged = false;
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

    public abstract IEnumerator Stun(float stuntime);
}
