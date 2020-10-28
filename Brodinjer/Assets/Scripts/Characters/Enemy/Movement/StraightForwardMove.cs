using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/NavMesh/Straight Forward")]
public class StraightForwardMove : NavMesh_Enemy_Base
{
    public float Acceleration;
    
    public override IEnumerator Move()
    {
        agent.speed = Speed;
        agent.acceleration = Acceleration;
        if (animation!= null)
            animation.StartAnimation();
        while (moving)
        {
            Debug.Log(agent.transform.forward * Time.deltaTime * Speed);
            if(agent.enabled)
                agent.destination += agent.transform.forward * Time.deltaTime * Speed;
            yield return fixedUpdate;
        }
    }

    public override Enemy_Movement GetClone()
    {
        StraightForwardMove temp = CreateInstance<StraightForwardMove>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.Acceleration = Acceleration;
        temp.animation = animation;
        return temp;
    }
}
