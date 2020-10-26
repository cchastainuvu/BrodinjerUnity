using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Jump_knockback : Knockback_CC
{
    public Animator anim;
    public string HitTrigger, NoHitTrigger;
    public ResetTriggers triggerreset;
    public float ActivateWaitTime;
    public bool active= true;
    
    /*private IEnumerator Awake()
    {
        yield return new WaitForSeconds(ActivateWaitTime);
        active = true;
    }*/

    protected override void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            _cc = other.GetComponent<CharacterController>();
            if (_cc == null)
                _cc = other.gameObject.GetComponentInParent<CharacterController>();
            if (_cc != null)
            {
                difference = _cc.transform.position - BaseObj.position;
                difference.y = 0;
                difference = (difference.normalized * thrust);
                anim.SetTrigger(HitTrigger);
                if (!running)
                {
                    StartCoroutine(KnockCo(_cc, difference));
                }
            }
        }

    }

    protected override IEnumerator KnockCo(CharacterController character, Vector3 impact)
    {
        //active = false;
        triggerreset.ResetAllTriggers();
        running = true;
        if (character != null)
        {
            onKnockback.Invoke();
            currentTime = knockTime;
            while (currentTime > 0)
            {
                character.Move(impact * Time.deltaTime);
                //impact = Vector3.Lerp(impact, Vector3.zero, knockTime * Time.deltaTime);
                currentTime -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        running = false;
        anim.ResetTrigger(HitTrigger);
        anim.ResetTrigger(NoHitTrigger);
        gameObject.SetActive(false);

    } 
}
