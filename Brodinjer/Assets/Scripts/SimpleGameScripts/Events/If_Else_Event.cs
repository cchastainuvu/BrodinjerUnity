using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class If_Else_Event : MonoBehaviour
{
    public UnityEvent TrueEvent, FalseEvent;

    public void Check(bool value)
    {
        if (value)
        {
            TrueEvent.Invoke();
        }
        else
        {
            FalseEvent.Invoke();
        }
    }
    
    public void Check(BoolData value)
    {
        Check(value.value);
    }
    
    public void Check(GameObject obj)
    {
        Check(obj.activeSelf);
    }
    
}
