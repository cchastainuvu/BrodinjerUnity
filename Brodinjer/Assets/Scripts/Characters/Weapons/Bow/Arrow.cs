using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public Rigidbody arrowRigid;
    private GameObject stickyObj;
    private Stickable stickable;
    public UnityEvent onCollision;
    public Transform Direction;
    public GameObject ArrowTip;
    private bool fired;
    public GameObject ArrowCollider;
    public ParticleSystem weaponTrail;
    public SoundController ImpactSound;
    private bool hit;

    private void Start()
    {
        ArrowTip.SetActive(false);
        if (arrowRigid)
        {
            arrowRigid.GetComponent<Rigidbody>();
        }

        fired = false;
        hit = false;

    }

    public void Fired()
    {
        fired = true;
        ArrowTip.SetActive(true);
        ArrowCollider.SetActive(true);
        weaponTrail.Play();
    }



    private void OnCollisionEnter(Collision other)
    {
        if (fired && !hit)
        {
            hit = true;
            weaponTrail.Stop();
            ImpactSound.Play();
            stickable = other.gameObject.GetComponentInChildren<Stickable>();
            if (stickable)
            {
                stickyObj = stickable.gameObject;
                arrowRigid.constraints = RigidbodyConstraints.FreezeAll;
                arrowRigid.gameObject.transform.parent = stickyObj.transform;
            }

            onCollision.Invoke();
        }

    }
}
