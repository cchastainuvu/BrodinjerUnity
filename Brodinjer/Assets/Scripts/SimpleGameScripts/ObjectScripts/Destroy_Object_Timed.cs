using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destroy_Object_Timed : MonoBehaviour
{
    public float waitTime;
    public bool OnAwake = false;

    private void Start()
    {
        if (OnAwake)
        {
            Call();
        }
    }

    public void Call()
    {
        StartCoroutine(DestoryTimer());
    }

    private IEnumerator DestoryTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
