using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationManager : MonoBehaviour
{
    public CameraRotationBase cameraRotation;

    private Coroutine swapFunc;
    private float currentTime;

    private float mouseX, mouseY;
    public string CameraHorizontal, CameraVertical;

    private bool moving;

    private Coroutine rotateFunc;

    private void Start()
    {
        StartRotation();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartRotation()
    {
        moving = true;
        rotateFunc = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (moving)
        {
            mouseX += Input.GetAxis(CameraHorizontal) * cameraRotation.rotationSpeed * Time.deltaTime;
            mouseY -= Input.GetAxis(CameraVertical) * cameraRotation.rotationSpeed * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, cameraRotation.minCamAngle, cameraRotation.maxCamAngle);
            transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            yield return new WaitForFixedUpdate();
        }
    }
    
    public void SwapRotation(CameraRotationBase newBase)
    {
        if(cameraRotation.cameraObject != null)
            cameraRotation.cameraObject.SetActive(false);
        if(newBase.cameraObject != null)
            newBase.cameraObject.SetActive(true);
        cameraRotation = newBase;
    }

    public void StopRotation()
    {
        moving = false;
        if(rotateFunc!= null)
            StopCoroutine(rotateFunc);
    }

    public void StartTimeSwap(float time, CameraRotationBase origCamera, CameraRotationBase newCamera)
    {
        if (swapFunc == null)
        {
            currentTime = time;
            SwapRotation(newCamera);
            swapFunc = StartCoroutine(TimedSwap(origCamera));
        }
        else
        {
            currentTime = time;
        }
        
    }

    private IEnumerator TimedSwap(CameraRotationBase origCamera)
    {
        while (currentTime > 0)
        {
            currentTime -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        StopTimeSwap(origCamera);
    }

    public void StopTimeSwap(CameraRotationBase origCamera)
    {
        if (swapFunc != null)
        {
            SwapRotation(origCamera);
            StopCoroutine(swapFunc);
        }
        else if(origCamera != cameraRotation)
        {
            SwapRotation(origCamera);
        }
        swapFunc = null;

    }
}
