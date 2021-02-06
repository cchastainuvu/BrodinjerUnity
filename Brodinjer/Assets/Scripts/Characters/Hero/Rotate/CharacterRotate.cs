using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterRotate : ScriptableObject
{
    public float  RotateSpeed;
    public string ForwardAxis, SideAxis;
    [HideInInspector]
    public bool canRotate;
    private Coroutine rotateFunc;
    protected Transform obj;
    protected Transform camera;

    public virtual void Init(Transform obj, Transform camera)
    {
        this.obj = obj;
        this.camera = camera;
    }

    public abstract IEnumerator Rotate();

}
