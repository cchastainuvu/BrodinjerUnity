using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Patrol_Destination : Enemy_Patrol
{
    public bool PingPongMovement;
    
    protected override void Init()
    {
        base.Init();
        if (destinations.Count > 0)
        {
            agent.destination = destinations[0].position;
            currentDestIndex = 0;
            idle = false;
        }
        else
        {
            idle = true;
        }
    }
    
    public override IEnumerator Move()
    {
        agent.updateRotation = true;
        while (moving)
        {
            if (CheckPosition(destinations[currentDestIndex].position))
            {
                StartCoroutine(ChangeDest());
            }

            yield return fixedUpdate;
        }
    }

    /*public override Enemy_Movement GetClone()
    {
        Enemy_Patrol_Destination temp = CreateInstance<Enemy_Patrol_Destination>();
        temp.PingPongMovement = PingPongMovement;
        temp.checkX = checkX;
        temp.checkY = checkY;
        temp.checkZ = checkZ;
        temp.DestinationOffset = DestinationOffset;
        temp.minChangeDestTime = minChangeDestTime;
        temp.maxChangeDestTime = maxChangeDestTime;
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        return temp;
    }*/

    public override IEnumerator ChangeDest()
    {
        if (destinations.Count <= 1)
        {
            agent.speed = 0;
            idle = true;
            yield return fixedUpdate;
        }
        else
        {
            if (positive)
            {
                currentDestIndex++;
                if (currentDestIndex > destinations.Count-1)
                {
                    if (PingPongMovement)
                    {
                        positive = false;
                        currentDestIndex -= 2;
                    }
                    else
                    {
                        currentDestIndex = 0;
                    }
                }
            }
            else
            {
                currentDestIndex--;
                if (currentDestIndex < 0)
                {
                    if (PingPongMovement)
                    {
                        positive = true;
                        currentDestIndex += 2;
                    }
                    else
                    {
                        currentDestIndex = destinations.Count - 1;
                    }
                }
            }

            agent.speed = 0;
            yield return new WaitForSeconds(ChangeTime());
            agent.destination = destinations[currentDestIndex].position;
            agent.speed = Speed;
        }
    }

}
