using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Script : MonoBehaviour
{
    public Transform LookAtObject;
    public float Speed;
    public bool OnAwake;
    private Coroutine lookatfunc;
    private bool looking;
    private Quaternion facingDirection;

    private void Start()
    {
        if (OnAwake)
        {
            StartLookAt();
        }
    }

    public void StartLookAt()
    {
        looking = true;
        lookatfunc = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        while (looking)
        {
            facingDirection = Quaternion.LookRotation((LookAtObject.transform.position - transform.position).normalized);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, facingDirection, Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopLookAt()
    {
        if(lookatfunc != null)
            StopCoroutine(lookatfunc);
        looking = false;
    }
    
    
    
}
