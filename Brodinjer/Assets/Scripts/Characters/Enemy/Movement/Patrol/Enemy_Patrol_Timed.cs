using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Patrol_Timed : Enemy_Patrol
{
    public float MinTime, MaxTime;
    private float timeleft;
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
        timeleft = Random.Range(MinTime, MaxTime);
        while (moving && timeleft > 0)
        {
            if (!idle && CheckPosition(destinations[currentDestIndex].position))
            {
                idle = true;
                agent.speed = 0;
            }

            timeleft -= .1f;
            yield return new WaitForSeconds(.1f);
            
        }
        if(moving)
            StartCoroutine(ChangeDest());
    }

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

            yield return new WaitForSeconds(ChangeTime());
            idle = false;
            agent.speed = Speed;
            agent.destination = destinations[currentDestIndex].position;
            StartCoroutine(Move());
        }
    }
}
