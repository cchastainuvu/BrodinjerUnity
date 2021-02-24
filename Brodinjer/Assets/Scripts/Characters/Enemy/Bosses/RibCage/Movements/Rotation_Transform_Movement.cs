using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Rotation_Transform_Movement : Transform_Movement_Base
{
    public float offset;
    public bool x, y, z;

    private Vector3 moveVector, target, currentPos;
    private Quaternion rotationDirection;

    public Transform Destination;

    public string SpeedFloatName;

    private float currentSpeed;

    public UnityEvent ReachCenter;
    private bool RunEvent = false;

    public SoundController walkSound;

    private bool walking;
    public float MinFootstep, MaxFootstep;

    public void SetRun(bool val)
    {
        RunEvent = val;
    }

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
            if (!walking)
            {
                walking = true;
                StartCoroutine(WalkSound());
            }
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, moveVector, Speed*Time.deltaTime);
            target = player.transform.position;
            target.y = 0;
            currentPos = enemy.transform.position;
            currentPos.y = 0;
            currentSpeed = (Vector3.MoveTowards(enemy.transform.position, moveVector, Speed * Time.deltaTime).magnitude);
            if (SpeedFloatName != "")
                anim.SetFloat(SpeedFloatName, currentSpeed);
            rotationDirection = Quaternion.LookRotation((target - currentPos).normalized);
            enemy.transform.rotation =
                Quaternion.Lerp(enemy.transform.rotation, rotationDirection, AngularSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        walking = false;
        if(RunEvent)
            ReachCenter.Invoke();
        RunEvent = false;
        if(SpeedFloatName != "" )
            anim.SetFloat(SpeedFloatName, 0);
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

    private IEnumerator WalkSound()
    {
        while (walking)
        {
            if (currentSpeed >= .1f)
            {
                walkSound.Play();
                yield return new WaitForSeconds(GeneralFunctions.ConvertRange(0, Speed,
                    MaxFootstep, MinFootstep, currentSpeed));
            }
            yield return new WaitForFixedUpdate();
        }
    }

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
