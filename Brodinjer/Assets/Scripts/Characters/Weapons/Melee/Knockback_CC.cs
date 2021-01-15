using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback_CC : MonoBehaviour
{
    public float thrust; //force
    public float knockTime; //time
    protected Vector3 difference;
    protected CharacterController _cc;
    public Transform BaseObj;
    protected bool running;
    protected float currentTime;
    public UnityEvent onKnockback;

    protected virtual void Start()
    {
        if (BaseObj == null)
        {
            BaseObj = transform;
        }

        running = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        _cc = other.GetComponent<CharacterController>();
        if (_cc == null)
            _cc = other.GetComponentInParent<CharacterController>();
        if (_cc != null)
        {
            difference = _cc.transform.position - BaseObj.position;
            difference.y = 0;
            difference = (difference.normalized * thrust);
            if(!running)
                StartCoroutine(KnockCo(_cc, difference));
        }

    }

    protected virtual IEnumerator KnockCo(CharacterController character, Vector3 impact)
    {
        running = true;
        if (character != null)
        {
            currentTime = knockTime;
            while (currentTime > 0)
            {
                character.Move(impact * Time.deltaTime);
                //impact = Vector3.Lerp(impact, Vector3.zero, knockTime * Time.deltaTime);
                currentTime -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            onKnockback.Invoke();
        }

        running = false;
    }

    private void OnDisable()
    {
        running = false;
    }
}
