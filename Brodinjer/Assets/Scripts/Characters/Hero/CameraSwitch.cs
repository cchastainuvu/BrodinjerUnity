using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class CameraSwitch : MonoBehaviour
{
    public CameraBase cameraScript;
    private Coroutine moveFunc;
    private Coroutine tempMoveFunc;
    private float currentTime, currentTimeCamSwap;
    private Coroutine swapFunc;
    private Camera_Manager newManager, oldManager;
    public CinemachineMixingCamera blendCam;
    public float CameraSwapTime;

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
        //newCam.cameraTransform.gameObject.SetActive(true);
        //cameraScript.cameraTransform.gameObject.SetActive(false);
        StartCoroutine(SetCameraWeights(cameraScript.camNum, newCam.camNum));
        /*newCam.canMove = true;
        tempMoveFunc = StartCoroutine(newCam.Move());
        cameraScript.canMove = false;
        if (moveFunc != null)
            StopCoroutine(moveFunc);
        moveFunc = tempMoveFunc;*/
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

    private IEnumerator SetCameraWeights(int OrigCamNum, int NewCamNum)
    {
        currentTimeCamSwap = 0;
        while (currentTimeCamSwap < CameraSwapTime)
        {
            blendCam.SetWeight(OrigCamNum, Mathf.Lerp(blendCam.GetWeight(OrigCamNum), 0,
                GeneralFunctions.ConvertRange(0,CameraSwapTime, 0,1, currentTimeCamSwap)));
            blendCam.SetWeight(NewCamNum, Mathf.Lerp(blendCam.GetWeight(NewCamNum), 1,
                GeneralFunctions.ConvertRange(0,CameraSwapTime, 0,1, currentTimeCamSwap)));
            currentTimeCamSwap += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    
}
