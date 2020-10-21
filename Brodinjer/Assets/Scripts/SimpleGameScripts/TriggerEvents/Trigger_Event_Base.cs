using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger_Event_Base : MonoBehaviour
{
    public UnityEvent Event;
    public enum Check { Layer, Tag, Name, }
    public Check checksFor;
    public float waitTime;

    public string objName;
    public LayerMask layer;
    public string tagName;

    public bool active = true;
    protected bool isRunning;

    private void Start()
    {
        isRunning = false;
    }

    public virtual IEnumerator CheckTrigger(Collider coll)
    {
        isRunning = true;
        switch (checksFor)
        {
            case Check.Layer:
                if (coll.gameObject.layer == ToLayer(layer.value))
                {
                    yield return new WaitForSeconds(waitTime);
                    RunEvent();
                }
                break;
            case Check.Name:
                if (coll.gameObject.name.Contains(objName))
                {
                    yield return new WaitForSeconds(waitTime);
                    RunEvent();
                }
                break;
            case Check.Tag:
                //Debug.Log(coll.gameObject.tag);
                if (coll.gameObject.CompareTag(tagName))
                {
                    //Debug.Log("Correct Tag");
                    yield return new WaitForSeconds(waitTime);
                    RunEvent();
                }
                break;
        }

        isRunning = false;
    }

    public virtual void RunEvent()
    {
        if(active)
            Event.Invoke();
    }
    
    public int ToLayer (int bitmask ) {
        int result = bitmask>0 ? 0 : 31;
        while( bitmask>1 ) {
            bitmask = bitmask>>1;
            result++;
        }
        return result;
    }

    public void Active(bool value)
    {
        active = value;
    }
    
}
