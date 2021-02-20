using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Single/bool")]
public class BoolData : SavableScriptableObjects
{
    public bool value;

    public void setBool(bool val)
    {
        value = val;
    }

    public override void SetObj(ScriptableObject obj)
    {
        BoolData temp = obj as BoolData;
        if (temp != null)
            value = temp.value;
    }

    public void Toggle()
    {
        value = !value;
    }
}
