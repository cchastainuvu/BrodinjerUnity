using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class Distance_Event : MonoBehaviour
{
    public UnityEvent EnterDistanceEvent, ExitDistanceEvent;
    public float minDistance, maxDistance;
    public float waitTime, timeInDistance;
    private bool checking;
    private bool inDistance;
    public Transform checkObj;
    public float offset;
    public bool checkOnAwake;
    public bool RunEventonInit;
    private Coroutine checkFunc;
    
    public bool checkY;

    private void Start()
    {
        if (checkOnAwake)
        {
            StartCheck();
        }
    }

    public void StartCheck()
    {
        if (!checking)
        {
            checking = true;
            if (CheckDistance())
            {
                inDistance = true;
                if (RunEventonInit)
                    StartCoroutine(EnterDistance());
            }
            else
            {
                inDistance = false;
                if (RunEventonInit)
                    StartCoroutine(ExitDistance());
            }

            checkFunc = StartCoroutine(Check());
        }
    }

    public void StopCheck()
    {
        checking = false;
        if(checkFunc != null)
            StopCoroutine(checkFunc);
    }

    private IEnumerator EnterDistance()
    {
        yield return new WaitForSeconds(waitTime);
        EnterDistanceEvent.Invoke();
    }

    private IEnumerator ExitDistance()
    {
        yield return new WaitForSeconds(timeInDistance);
        if (inDistance)
        {
            yield return new WaitForSeconds(waitTime);
            ExitDistanceEvent.Invoke();
        }
    }

    private IEnumerator Check()
    {
        while (checking)
        {
            if (CheckDistance())
            {
                if (!inDistance)
                {
                    inDistance = true;
                    StartCoroutine(EnterDistance());
                }
            }
            else
            {
                if (inDistance)
                {
                    inDistance = false;
                    StartCoroutine(ExitDistance());
                }
                
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private bool CheckDistance()
    {
        if (DistanceFormula(transform.position, checkObj.position) > minDistance - offset
            && DistanceFormula(transform.position, checkObj.position) < maxDistance + offset)
        {
            return true;
        }

        return false;
    }

    private double DistanceFormula(Vector3 vector1, Vector3 vector2)
    {
        double distance;
        if (checkY)
        {
            distance =  Math.Sqrt(Math.Pow((vector2.x - vector1.x), 2) + Math.Pow((vector2.y - vector1.y), 2) +
                             Math.Pow((vector2.z - vector1.z), 2));
        }
        else
        {
            distance = Math.Sqrt(Math.Pow((vector2.x - vector1.x), 2) +
                                 Math.Pow((vector2.z - vector1.z), 2));
        }
        return distance;
    }


}
