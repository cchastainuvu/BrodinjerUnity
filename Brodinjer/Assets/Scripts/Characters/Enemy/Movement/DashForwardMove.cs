using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/NavMesh/Dash Forward")]

public class DashForwardMove : NavMesh_Enemy_Base
{
    public float DashSpeed;
    public float DashMinTime, DashMaxTime;
    public float Acceleration;
    private float currentTime, dashTime;
    
    public override IEnumerator Move()
    {
        if(animation != null)
            animation.StartAnimation();
        dashTime = Random.Range(DashMinTime, DashMaxTime);
        currentTime = 0;
        agent.speed = DashSpeed;
        if(agent.enabled)
            agent.destination += agent.transform.forward * Time.deltaTime * DashSpeed;
        Debug.Log("Start Dash");
        while (currentTime < dashTime && canMove && moving)
        {
            currentTime += Time.deltaTime;
            if(agent.enabled)
                agent.destination += agent.transform.forward * Time.deltaTime * DashSpeed;
            yield return fixedUpdate;
        }
        Debug.Log("Finish Dash");
        agent.speed = Speed;
        while (canMove && moving)
        {
            if(agent.enabled)
                agent.destination += agent.transform.forward * Time.deltaTime * DashSpeed;
            yield return fixedUpdate;
        }
        yield return fixedUpdate;
    }

    public override Enemy_Movement GetClone()
    {
        DashForwardMove temp = CreateInstance<DashForwardMove>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.Acceleration = Acceleration;
        temp.animation = animation;
        temp.DashMinTime = DashMinTime;
        temp.DashMaxTime = DashMaxTime;
        temp.DashSpeed = DashSpeed;
        return temp;
    }
}
