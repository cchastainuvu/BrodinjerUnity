using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Attack : MonoBehaviour
{
    public Enemy_Attack_Base Attack;
    public Enemy_Manager Manager;
    public float minDistance, maxDistance;
    private bool checking;
    private bool inDistance;
    public float offset;
    private Coroutine checkFunc;
    public bool checkX = true, checkY = false, checkZ = true;
    private Transform obj;
    public bool CheckOnAwake;

    private void Start()
    {
        obj = FindObjectOfType<PlayerMovement>().transform;
        if(CheckOnAwake)
            StartCheck();
    }

    public void StartCheck()
    {
        if (!checking)
        {
            checking = true;
            inDistance = false;
            checkFunc = StartCoroutine(Check());
        }
    }

    public void StopCheck()
    {
        checking = false;
        if(checkFunc != null)
            StopCoroutine(checkFunc);
    }

    private IEnumerator Check()
    {
        while (checking)
        {
            if (CheckDistance())
            {
                if (!Attack.attacking)
                {
                    Manager.StartAttack();
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private bool CheckDistance()
    {
        if (DistanceFormula(transform.position, obj.position) > minDistance - offset
            && DistanceFormula(transform.position, obj.position) < maxDistance + offset)
        {
            return true;
        }

        return false;
    }

    private double DistanceFormula(Vector3 vector1, Vector3 vector2)
    {
        double distance;
        double ydist = 0;
        double xdist = 0;
        double zdist= 0;

        xdist = (checkX) ? Math.Pow((vector2.x - vector1.x), 2) : 0;
        ydist = (checkY) ? Math.Pow((vector2.y - vector1.y), 2) : 0;
        zdist = (checkZ) ? Math.Pow((vector2.z - vector1.z), 2) : 0;

        distance =  Math.Sqrt(xdist + ydist + zdist);
        return distance;
    }
}
