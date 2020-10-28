using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Patrol : Enemy_Movement
{
    protected bool positive = true;
    public float minChangeDestTime, maxChangeDestTime;
    public float DestinationOffset;
    public bool checkX, checkY, checkZ;
    protected int currentDestIndex;


    public virtual bool CheckPosition(Vector3 position)
    {
        if ((!checkX ||
             (enemy.transform.position.x >= position.x - DestinationOffset
              && enemy.transform.position.x <= position.x + DestinationOffset))
            && (!checkY ||
                (enemy.transform.position.y >= position.y - DestinationOffset
                 && enemy.transform.position.y <= position.y + DestinationOffset))
            && (!checkZ ||
                (enemy.transform.position.z >= position.z - DestinationOffset
                 && enemy.transform.position.z <= position.z + DestinationOffset)))
        {
            return true;
        }
        return false;
    }

    public abstract IEnumerator ChangeDest();

    public virtual float ChangeTime()
    {
        return Random.Range(minChangeDestTime, maxChangeDestTime);
    }
}
