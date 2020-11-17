using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Single/float/float")]
public class FloatData : SavableScriptableObjects
{
    public float value;
    
    public virtual void SetFloat(float value)
    {
        this.value = value;
    }

    public virtual void AddFloat(float value)
    {
        this.value += value;
    }

    public virtual void SubFloat(float value)
    {
        this.value -= value;
    }

    public virtual FloatData GetClone()
    {
        FloatData temp = CreateInstance<FloatData>();
        temp.value = value;
        return temp;
    }

    public override void SetObj(ScriptableObject obj)
    {
        FloatData temp = obj as FloatData;
        if (temp != null)
        {
            value = temp.value;
        }
    }
}
