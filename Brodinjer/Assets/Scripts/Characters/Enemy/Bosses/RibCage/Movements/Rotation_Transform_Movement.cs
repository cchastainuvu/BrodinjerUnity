using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotation_Transform_Movement : Transform_Movement_Base
{
    public float offset;
    public bool x, y, z;

    private Vector3 moveVector, target, currentPos;
    private Quaternion rotationDirection;

    public Transform Destination;

    public override IEnumerator Move()
    {
        moveVector = enemy.transform.position;
        if (x)
            moveVector.x = Destination.position.x;
        if (y)
            moveVector.y = Destination.position.y;
        if (z)
            moveVector.z = Destination.position.z;
        while (!CheckDestination(enemy.transform.position, Destination.transform.position, offset))
        {      
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveVector, Speed*Time.deltaTime);
            target = player.transform.position;
            target.y = 0;
            currentPos = enemy.transform.position;
            currentPos.y = 0;
            rotationDirection = Quaternion.LookRotation((target - currentPos).normalized);
            enemy.transform.rotation =
                Quaternion.Lerp(enemy.transform.rotation, rotationDirection, AngularSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        enemy.transform.position = moveVector;

        while (moving)
        {
            target = player.transform.position;
            target.y = 0;
            currentPos = enemy.transform.position;
            currentPos.y = 0;
            rotationDirection = Quaternion.LookRotation((target - currentPos).normalized);
            enemy.transform.rotation =
                Quaternion.Lerp(enemy.transform.rotation, rotationDirection, AngularSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    /*public override Enemy_Movement GetClone()
    {
        Rotation_Transform_Movement temp = CreateInstance<Rotation_Transform_Movement>();
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        temp.animation = animation;
        temp.offset = offset;
        temp.x = x;
        temp.y = y;
        temp.z = z;
        return temp;
    }*/

    public bool CheckDestination(Vector3 Dest01, Vector3 Dest02, float offset)
    {
        if ((!x || (Dest01.x >= Dest02.x - offset
             && Dest01.x <= Dest02.x + offset))
            &&(!y || (Dest01.y >= Dest02.y - offset
               && Dest01.y <= Dest02.y + offset))
            &&(!z || (Dest01.z >= Dest02.z - offset
               && Dest01.z <= Dest02.z + offset)))
        {
            return true;
        }
        return false;
    }
}
