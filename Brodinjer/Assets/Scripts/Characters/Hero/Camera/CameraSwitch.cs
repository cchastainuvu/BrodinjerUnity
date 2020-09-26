using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraSwitch : MonoBehaviour
{
    public CameraBase cameraScript;
    private Coroutine moveFunc;
    private Coroutine tempMoveFunc;
    private float currentTime;
    private Coroutine swapFunc;

    private void Start()
    {
        //yield return new WaitForSeconds(.1f);
        StartMove();
    }

    public void StartMove()
    {
        cameraScript.canMove = true;
        moveFunc = StartCoroutine(cameraScript.Move());
    }

    public void SwapCamera(CameraBase newCam)
    {
        newCam.cameraTransform.gameObject.SetActive(true);
        cameraScript.cameraTransform.gameObject.SetActive(false);
        newCam.canMove = true;
        tempMoveFunc = StartCoroutine(newCam.Move());
        cameraScript.canMove = false;
        if (moveFunc != null)
            StopCoroutine(moveFunc);
        moveFunc = tempMoveFunc;
        cameraScript = newCam;
    }

    public void StartTimeSwap(float time, CameraBase origCamera, CameraBase newCamera)
    {
        if (swapFunc == null)
        {
            currentTime = time;
            SwapCamera(newCamera);
            swapFunc = StartCoroutine(TimedSwap(origCamera));
        }
        else
        {
            currentTime = time;
        }
        
    }
    

    private IEnumerator TimedSwap(CameraBase origCamera)
    {
        while (currentTime > 0)
        {
            currentTime -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        StopTimeSwap(origCamera);
    }

    public void StopTimeSwap(CameraBase origCamera)
    {
        if (swapFunc != null)
        {
            SwapCamera(origCamera);
            StopCoroutine(swapFunc);
        }
        else if(origCamera != cameraScript)
        {
            SwapCamera(origCamera);
        }
        swapFunc = null;

    }

    public void StopMove()
    {
        cameraScript.canMove = false;
        if(moveFunc != null)
            StopCoroutine(moveFunc);
    }
    
}
