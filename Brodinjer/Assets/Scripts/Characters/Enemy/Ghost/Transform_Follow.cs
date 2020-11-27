using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Transform_Follow : Transform_Movement_Base
{
    public bool lookAtFollow;
    private Vector3 targetRotation, targetDestination;
    private Quaternion facingDirection;
    public float offset;
    public bool x, y, z;
    public float ySpeed;
    public float distanceFromTarget;
    public bool rotateInPlace;
    
    public override IEnumerator Move()
    {
        if(AnimationBase!= null)
            AnimationBase.StartAnimation();
        while (moving)
        {
            if (lookAtFollow && moving)
            {
                    targetRotation = player.transform.position;
                    targetRotation = (targetRotation - enemy.transform.position).normalized;
                    facingDirection = Quaternion.LookRotation(targetRotation);
                    Quaternion YRotation = Quaternion.Euler(enemy.transform.rotation.eulerAngles.x,
                        facingDirection.eulerAngles.y, enemy.transform.rotation.eulerAngles.z);
                    if (!GeneralFunctions.CheckDestination(enemy.transform.rotation.eulerAngles,
                        YRotation.eulerAngles, offset))
                    {
                        enemy.transform.rotation =
                            Quaternion.Lerp(enemy.transform.rotation, YRotation, AngularSpeed * Time.deltaTime);
                    }
            }

            if (rotateInPlace)
            {
                targetDestination = enemy.transform.position;
                if (x)
                {
                    targetDestination.x = player.position.x;
                }

                if (y)
                {
                    targetDestination.y = player.position.y;
                }

                if (z)
                {
                    targetDestination.z = player.position.z;
                }

                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, targetDestination, Speed * Time.deltaTime);
            }
            else if(lookAtFollow)
            {
                targetDestination = enemy.transform.position + (enemy.transform.forward * Speed * Time.deltaTime);
                targetDestination.y = enemy.transform.position.y;
                if (!x)
                {
                    targetDestination.x = enemy.transform.position.x;
                }

                if (!z)
                {
                    targetDestination.z = enemy.transform.position.z;
                }

                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, targetDestination, Speed * Time.deltaTime);

                if (y)
                {
                    targetDestination = enemy.transform.position;
                    targetDestination.y = player.position.y;
                    enemy.transform.position =
                        Vector3.MoveTowards(enemy.transform.position, targetDestination, ySpeed * Time.deltaTime);
                }
            }
            yield return fixedUpdate;

        }
        AnimationBase.StopAnimation();
    }

}
