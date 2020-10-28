using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback_CC : MonoBehaviour
{

    public float thrust; //force
    public float knockTime; //time
    private Vector3 difference;
    private CharacterController _cc;
    public Transform BaseObj;
    private bool running;
    private float currentTime;
    public UnityEvent onKnockback;

    private void Start()
    {
        if (BaseObj == null)
        {
            BaseObj = transform;
        }

        running = false;
    }

    private void OnTriggerEnter(Collider other)
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

    private IEnumerator KnockCo(CharacterController character, Vector3 impact)
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
}
