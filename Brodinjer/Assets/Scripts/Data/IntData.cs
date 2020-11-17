using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Single/int/int")]
public class IntData : SavableScriptableObjects
{
    public int value;

    public virtual void SetInt(int value)
    {
        this.value = value;
    }

    public virtual void AddInt(int value)
    {
        this.value += value;
    }

    public virtual void SubInt(int value)
    {
        this.value -= value;
    }

    public override void SetObj(ScriptableObject obj)
    {
        IntData temp = obj as IntData;
        if (temp != null)
        {
            value = temp.value;
        }
    }
}
