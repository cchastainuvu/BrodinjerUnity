using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Single/Transform")]
public class TransformData : SavableScriptableObjects
{
    public Transform transform;

    public void SetTransform(Transform transform)
    {
        this.transform = transform == null ? null : transform;
    }

    public void SetTransform(GameObject obj)
    {
        this.transform = obj == null ? null : obj.transform;
    }

    public override void SetObj(ScriptableObject obj)
    {
        TransformData temp = obj as TransformData;
        if (temp != null)
        {
            transform = temp.transform;
        }
    }
}
