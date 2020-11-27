using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Patrol_Flee : Enemy_Patrol
{
    private Vector3 backDest, forwardDest;
    
    public override IEnumerator Move()
    {
        agent.SetDestination(destinations[0].position);
        currentDestIndex = 0;
        agent.speed = Speed;
        agent.updateRotation = false;
        while (moving)
        {
            if (CheckPosition(destinations[currentDestIndex].position))
            {
                StartCoroutine(ChangeDest());
            }
            yield return fixedUpdate;
        }
    }

    public override IEnumerator ChangeDest()
    {
        if (currentDestIndex <= 0)
        {
            backDest = destinations[destinations.Count-1].position;
            forwardDest = destinations[currentDestIndex + 1].position;
        }
        else if (currentDestIndex >= destinations.Count - 1)
        {
            backDest = destinations[currentDestIndex - 1].position;
            forwardDest = destinations[0].position;
        }
        else
        {
            backDest = destinations[currentDestIndex - 1].position;
            forwardDest = destinations[currentDestIndex + 1].position;
        }

        if (GetDistance(forwardDest, player.transform.position) >
            GetDistance(backDest, player.transform.position))
        {
            currentDestIndex++;
            if (currentDestIndex >= destinations.Count)
            {
                currentDestIndex = 0;
            }
        }
        else
        {
            currentDestIndex--;
            if (currentDestIndex < 0)
            {
                currentDestIndex = destinations.Count - 1;
            }
        }

        agent.speed = 0;
        if (agent.enabled)
            agent.SetDestination(destinations[currentDestIndex].position);
        yield return new WaitForSeconds(Random.Range(minChangeDestTime, maxChangeDestTime));
        agent.speed = Speed;
    }

    private float GetDistance(Vector3 dest01, Vector3 dest02)
    {
        return Mathf.Sqrt((Mathf.Pow((dest01.x - dest02.x), 2) + (Mathf.Pow((dest01.z - dest02.z), 2))));
    }
}
