using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Single/Vector3")]
public class Vector3Data : SavableScriptableObjects
{
    public Vector3 vector;

    public void SetVector(Transform obj)
    {
        vector = obj.position;
    }

    public void SetVector(GameObject obj)
    {
        vector = obj.transform.position;
    }

    public void SetVector(Vector3 vector)
    {
        this.vector = vector;
    }

    public override void SetObj(ScriptableObject obj)
    {
        Vector3Data temp = obj as Vector3Data;
        if (temp != null)
        {
            vector = temp.vector;
        }
    }

    public void SetPosition(Transform trans)
    {
        vector = trans.position;
    }

    public void SetRotation(Transform trans)
    {
        vector = trans.rotation.eulerAngles;
    }
    
}
