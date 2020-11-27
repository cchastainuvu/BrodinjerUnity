using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_Route : Transform_Movement_Base
{
    public List<Transform> destinations;
    private int currentDestIndex;
    private Transform currentDestination;
    private Vector3 targetRotation, targetDestination;
    private Quaternion facingDirection;
    public float RotationOffset, TranslationOffset;
    public bool x, y, z;
    public float ySpeed;

    public override IEnumerator Move()
    {
        if (AnimationBase != null)
            AnimationBase.StartAnimation();
        currentDestination = destinations[0];
        currentDestIndex = 0;
        while (moving)
        {
            targetRotation = currentDestination.transform.position;
            targetRotation = (targetRotation - enemy.transform.position).normalized;
            facingDirection = Quaternion.LookRotation(targetRotation);
            Quaternion YRotation = Quaternion.Euler(enemy.transform.rotation.eulerAngles.x,
                facingDirection.eulerAngles.y, enemy.transform.rotation.eulerAngles.z);
            if (!GeneralFunctions.CheckDestination(enemy.transform.rotation.eulerAngles,
                YRotation.eulerAngles, RotationOffset))
            {
                enemy.transform.rotation =
                    Quaternion.Lerp(enemy.transform.rotation, YRotation, AngularSpeed * Time.deltaTime);
            }

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
                targetDestination.y = currentDestination.position.y;
                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, targetDestination, ySpeed * Time.deltaTime);
            }

            if (CheckDestination(enemy.transform.position, currentDestination.position, TranslationOffset, x, y, z))
            {
                ChangeDest();
            }

            yield return fixedUpdate;

        }

        AnimationBase.StopAnimation();
    }

    private void ChangeDest()
    {
        currentDestIndex++;
        if (currentDestIndex >= destinations.Count)
        {
            currentDestIndex = 0;
        }

        currentDestination = destinations[currentDestIndex];
    }
    
    private bool CheckDestination(Vector3 Dest01, Vector3 Dest02, float offset, bool x, bool y, bool z)
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
