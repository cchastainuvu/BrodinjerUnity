using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Camera/RotationData")]
public class CameraRotationBase : ScriptableObject
{
    public float minCamAngle, maxCamAngle;
    public float rotationSpeed;
    [HideInInspector]public GameObject cameraObject;

    public void SetCamera(GameObject camObj)
    {
        cameraObject = camObj;
    }

}
