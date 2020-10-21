using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Targeting : MonoBehaviour
{
    //On Player
    [HideInInspector]public List<GameObject> EnemiesInRange;
    public Camera CamTrans;
    private List<GameObject> CheckEnemies;
    private Vector3 CamPos;
    public bool targeting;
    private bool running;
    private GameObject currentTarget;
    private float minDistance, distance;
    private int minIndex;
    private Vector3 lookatvector;
    public GameObject targetIndicator;
    private Quaternion quat;
    public TransformData currTargetObj;
    private GameObject objtarget;
    private List<Vector3> enemyarea;
    private Vector3 objPosition, subVector;

    private enum Placement
    {
        right,
        center,
        left
    };

    private void Start()
    {
        currTargetObj.transform = null;
        EnemiesInRange = new List<GameObject>();
        enemyarea = new List<Vector3>();
        running = true;
        targeting = false;
        if (targetIndicator != null)
        {
            targetIndicator.SetActive(false);
            targetIndicator.transform.parent = null;
        }

        subVector = new Vector3(.5f, .5f, 0);
        StartCoroutine(TargetFunction());
    }
    
    private void Closest(List<GameObject> objectsToCheck, Placement placement, GameObject currentDest = null)
    {
        if (objectsToCheck.Count <= 0)
        {
            currTargetObj.transform = null;
            currentTarget = null;
            targeting = false;
            targetIndicator.transform.parent = null;
            targetIndicator.SetActive(false);
            return;
        }
        else if (objectsToCheck.Count == 1)
        {
            currentTarget = objectsToCheck[0];
            targetIndicator.SetActive(true);
            targetIndicator.transform.parent = objectsToCheck[0].transform;
            targetIndicator.transform.position = objectsToCheck[0].transform.position;
            return;
        }
            enemyarea.Clear();
            foreach (var enemy in objectsToCheck)
            {
                objPosition = CamTrans.WorldToViewportPoint(enemy.transform.position);
                objPosition -= subVector;
                objPosition.z = 0;
                enemyarea.Add(objPosition);
            }

        if (currentDest != null)
        {
            objPosition = CamTrans.WorldToViewportPoint(currentDest.transform.position);
            objPosition -= subVector;
            objPosition.z = 0;
        }

        minDistance = enemyarea[0].magnitude;
        minIndex = 0;
        for (int i = 1; i < objectsToCheck.Count; i++)
        {
            switch (placement)
            {
                case Placement.center:
                    if (enemyarea[i].magnitude < minDistance)
                    {
                        minDistance = enemyarea[i].magnitude;
                        minIndex = i;
                    }

                    break;
                case Placement.left:
                    if (enemyarea[i].magnitude < minDistance && currentDest != null && enemyarea[i].x < objPosition.x)
                    {
                        minDistance = enemyarea[i].magnitude;
                        minIndex = i;
                    }

                    break;
                case Placement.right:
                    if (enemyarea[i].magnitude < minDistance && currentDest != null && enemyarea[i].x > objPosition.x)
                    {
                        minDistance = enemyarea[i].magnitude;
                        minIndex = i;
                    }

                    break;

            }
        }

        targetIndicator.transform.parent = objectsToCheck[minIndex].transform;
        targetIndicator.transform.position = objectsToCheck[minIndex].transform.position;
        currentTarget = objectsToCheck[minIndex];
        targetIndicator.SetActive(true);
        objtarget = currentTarget.GetComponentInChildren<TargetObject>().gameObject;
        targetIndicator.transform.position = objtarget.transform.position;
    }
    

    private void findClosest()
    {
        Closest(EnemiesInRange, Placement.center);
    }

    private void findRight(GameObject ignoreObj = null)
    {
        if (EnemiesInRange.Count <= 1)
        {
            currTargetObj.transform = null;
            return;
        }
        CheckEnemies = EnemiesInRange;
        if (ignoreObj != null)
            CheckEnemies.Remove(ignoreObj);
        Closest(CheckEnemies, Placement.right, ignoreObj);
    }
    
    private void findLeft(GameObject ignoreObj = null)
    {
        if (EnemiesInRange.Count <= 1)
        {
            currTargetObj.transform = null;
            return;
        }
        CheckEnemies = EnemiesInRange;
        if (ignoreObj != null)
            CheckEnemies.Remove(ignoreObj);
        Closest(CheckEnemies, Placement.left, ignoreObj);
    }

    private IEnumerator TargetFunction()
    {
        while (running)
        {
            yield return new WaitUntil(()=>Input.GetButtonDown("Target"));
            yield return new WaitForFixedUpdate();
            findClosest();
            if (currentTarget!= null && currentTarget.GetComponentInChildren<TargetObject>()!= null)
            {
                objtarget = currentTarget.GetComponentInChildren<TargetObject>().gameObject;
                currTargetObj.SetTransform(objtarget.transform);
                targeting = true;

            }
            else if(currentTarget != null)
            {
                objtarget = currentTarget;
                currTargetObj.SetTransform(currentTarget);
                targeting = true;

            }
            while (targeting && currentTarget != null)
            {
                lookatvector = objtarget.transform.position - transform.position;
                lookatvector.y = 0;
                quat = Quaternion.LookRotation(lookatvector);
                transform.rotation = quat;
                if (Input.GetButtonDown("Target"))
                {
                    targetIndicator.SetActive(false);
                    targetIndicator.transform.parent = null;
                    targeting = false;
                    currTargetObj.transform = null;
                }
                else if (!EnemiesInRange.Contains(currentTarget))
                {
                    findClosest();
                    if (currentTarget!= null && currentTarget.GetComponentInChildren<TargetObject>()!= null)
                    {
                        //Debug.Log("Different Target");
                        objtarget = currentTarget.GetComponentInChildren<TargetObject>().gameObject;
                        currTargetObj.SetTransform(objtarget.transform);
                    }
                    else
                    {
                        currTargetObj.SetTransform(currentTarget);
                    }
                    //currTargetObj.SetTransform(currentTarget);
                }
                else if (Input.GetButtonDown("TargetChange"))
                {
                    if (Input.GetAxisRaw("TargetChange") > 0)
                    {
                        findRight(currentTarget);
                        if (currentTarget!= null && currentTarget.GetComponentInChildren<TargetObject>()!= null)
                        {
                            //Debug.Log("Different Target");
                            objtarget = currentTarget.GetComponentInChildren<TargetObject>().gameObject;
                            currTargetObj.SetTransform(objtarget.transform);
                        }
                        else
                        {
                            currTargetObj.SetTransform(currentTarget);
                        }
                    }
                    else
                    {
                        findLeft(currentTarget); 
                        if (currentTarget!= null && currentTarget.GetComponentInChildren<TargetObject>()!= null)
                        {
                            //Debug.Log("Different Target");
                            objtarget = currentTarget.GetComponentInChildren<TargetObject>().gameObject;
                            currTargetObj.SetTransform(objtarget.transform);
                        }
                        else
                        {
                            currTargetObj.SetTransform(currentTarget);
                        }
                    }
                }
                yield return new WaitForSeconds(.01f);
            }

        }
    }
}
