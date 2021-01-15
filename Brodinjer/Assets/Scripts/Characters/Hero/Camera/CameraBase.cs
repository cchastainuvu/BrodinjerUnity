using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBase : ScriptableObject
{
    [HideInInspector]
    public bool canMove;
    protected float mouseX, mouseY;
    public float rotationSpeed;
    public string CameraHorizontal = "Mouse X", CameraVertical = "Mouse Y";
    public int camNum;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Transform FollowObj; //Attached to Rotate Object, out as far as Camera
    [HideInInspector]
    public Transform RotateObject; // Centered at Player


    protected Transform followObj;

    public virtual void Init(Transform cam, Transform follow, Transform rotate)
    {
        cameraTransform = cam;
        FollowObj = follow;
        RotateObject = rotate;
    }
  
    public abstract IEnumerator Move();


}
