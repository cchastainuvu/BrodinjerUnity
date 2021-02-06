using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[CreateAssetMenu(menuName = "Camera/RotationData")]
public class CameraRotationBase : ScriptableObject
{
    public float minCamAngle, maxCamAngle;
    public float rotationSpeed;
    public float mouseXMultiplier, mouseYMultiplier;
    public string DirectionFloatName;
    public Vector3 targetOffset;
    public float ShakeMaxAmplitude, ShakeMaxFrequency;
    [HideInInspector]public GameObject cameraObject;
    [HideInInspector] public CinemachineVirtualCamera vc;
    private Cinemachine3rdPersonFollow transposer;
    private CinemachineBasicMultiChannelPerlin shake;
    [HideInInspector] public bool targeting, shaking=false;


    public void SetCamera(GameObject camObj)
    {
        cameraObject = camObj;
        vc = camObj.GetComponent<CinemachineVirtualCamera>();
        if (vc != null)
        {
            transposer = vc.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            shake = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        SetShake(0, 0);
    }

    public void Target(GameObject lookat)
    {
        targeting = true;
    }

    public void Untarget()
    {
        targeting = false;
    }

    public void SetShake(float amplitude, float frequency)
    {
        if(shake!= null)
        {
            shake.m_AmplitudeGain = amplitude;
            shake.m_FrequencyGain = frequency;
        }
    }


}
