using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character/Enemy/Movement/Patrol/Random")]
public class Enemy_Patrol_Random : Enemy_Patrol
{
    public bool useX, useY, useZ;
    private Vector3 randomVector;

    private float minx, maxx, miny, maxy, minz, maxz;

    public void FindMinMax()
    {
        if (destinations.Count == 0)
        {
            minx = enemy.transform.position.x;
            maxx = minx;
            miny = enemy.transform.position.y;
            maxy = miny;
            minz = enemy.transform.position.z;
            maxz = minz;
        }
        else if (destinations.Count > 0)
        {
            minx = destinations[0].transform.position.x;
            maxx = minx;
            miny = destinations[0].transform.position.y;
            maxy = miny;
            minz = destinations[0].transform.position.z;
            maxz = minz;

            if (destinations.Count > 1)
            {
                for (int i = 1; i < destinations.Count; i++)
                {
                    if (destinations[i].position.x > maxx)
                    {
                        maxx = destinations[i].position.x;
                    }
                    else if (destinations[i].position.x < minx)
                    {
                        minx = destinations[i].position.x;
                    }
                    if (destinations[i].position.y > maxy)
                    {
                        maxy = destinations[i].position.y;
                    }
                    else if (destinations[i].position.y < miny)
                    {
                        miny = destinations[i].position.y;
                    }
                    if (destinations[i].position.z > maxz)
                    {
                        maxz = destinations[i].position.x;
                    }
                    else if (destinations[i].position.z < minz)
                    {
                        minz = destinations[i].position.x;
                    }
                }
            }
        }
        //Debug.Log("Min: (" + minx + ", " + miny + ", " + minz + ")");
        //Debug.Log("Max: (" + maxx + ", " + maxy + ", " + maxz + ")");

    }
      
    protected override void Init(NavMeshAgent agent, MonoBehaviour caller, List<Transform> destinations, Animator anim)
    {
        base.Init(agent, caller, destinations, anim); 
        FindMinMax();
        randomVector = enemy.transform.position;
        if(useX)
            randomVector.x = Random.Range(minx, maxx);
        if(useY)
            randomVector.y = Random.Range(miny, maxy);
        if(useZ)
            randomVector.z = Random.Range(minz, maxz);
        agent.destination = randomVector;
    }

    public override IEnumerator Move()
    {
        agent.speed = Speed;
        agent.updateRotation = true;
        while (moving)
        {
            //Debug.Log("Random Vector: " + randomVector);
            //Debug.Log("Enemy Vector: " + enemy.transform.position);
            if (CheckPosition(randomVector))
            {
                caller.StartCoroutine(ChangeDest());
            }

            yield return new WaitForSeconds(.1f);
            
        }
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Patrol_Random temp = CreateInstance<Enemy_Patrol_Random>();
        temp.useX = useX;
        temp.useY = useY;
        temp.useZ = useZ;
        temp.checkX = checkX;
        temp.checkY = checkY;
        temp.checkZ = checkZ;
        temp.DestinationOffset = DestinationOffset;
        temp.minChangeDestTime = minChangeDestTime;
        temp.maxChangeDestTime = maxChangeDestTime;
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        return temp;
    }

    public override IEnumerator ChangeDest()
    {
        agent.speed = 0;
        yield return new WaitForSeconds(ChangeTime());
        if(useX)
            randomVector.x = Random.Range(minx, maxx);
        if(useY)
            randomVector.y = Random.Range(miny, maxy);
        if(useZ)
            randomVector.z = Random.Range(minz, maxz);
        agent.destination = randomVector;
        agent.speed = Speed;
    }
}
