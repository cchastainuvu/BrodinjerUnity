using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public CameraBase cameraScript;
    private Coroutine moveFunc;
    public Transform followObj;
    public Transform rotateObj;
    public bool active = false;


    
    private void Awake()
    {
        cameraScript.Init(transform, followObj, rotateObj);
        gameObject.SetActive(active);
    }

    public void StartMove()
    {
        cameraScript.canMove = true;
        moveFunc = StartCoroutine(cameraScript.Move());
    }
    
    public void StopMove()
    {
        cameraScript.canMove = false;
        if(moveFunc != null)
            StopCoroutine(moveFunc);
    }

    




    
    
}
