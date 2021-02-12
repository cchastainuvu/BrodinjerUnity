using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Collision_Event_Base : MonoBehaviour
{
    public UnityEvent Event;
    public enum Check { Layer, Tag, Name, }
    public Check checksFor;
    public float waitTime;

    public string objName;
    public LayerMask layer;
    public string tagName;
    
    public int ToLayer (int bitmask ) {
        int result = bitmask>0 ? 0 : 31;
        while( bitmask>1 ) {
            bitmask = bitmask>>1;
            result++;
        }
        return result;
    }

    public virtual IEnumerator CheckCollision(Collision coll)
    {
        switch (checksFor)
        {
            case Check.Layer:
                if (((1 << coll.gameObject.layer) & layer) != 0)
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
                if (coll.gameObject.CompareTag(tagName))
                {
                    yield return new WaitForSeconds(waitTime);
                    RunEvent();
                }
                break;
        }
    }

    public virtual void RunEvent()
    {
        Event.Invoke();
    }
}
