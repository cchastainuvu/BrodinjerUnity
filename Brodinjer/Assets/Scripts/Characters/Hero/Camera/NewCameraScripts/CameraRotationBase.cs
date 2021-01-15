﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Camera/RotationData")]
public class CameraRotationBase : ScriptableObject
{
    public float minCamAngle, maxCamAngle;
    public float rotationSpeed;
    public float mouseXMultiplier, mouseYMultiplier;
    public string DirectionFloatName;
    [HideInInspector]public GameObject cameraObject;

    public void SetCamera(GameObject camObj)
    {
        cameraObject = camObj;
    }

}
