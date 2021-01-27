using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    public float MaxVelocity, Acceleration, Deceleration;
    public Transform Position01, Position02;
    private float currentSpeed;
    public Transform Hand;
    private Vector3 movement;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        StartMove();
    }

    public void StartMove()
    {
        currentSpeed = 0;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            while (!GeneralFunctions.CheckDestination(Hand.position, Position01.position, true, false, false, .1f))
            {
                if (currentSpeed < MaxVelocity)
                {
                    currentSpeed += Acceleration * Time.deltaTime;
                }

                if (currentSpeed > MaxVelocity)
                {
                    currentSpeed = MaxVelocity;
                }

                Hand.position = Vector3.MoveTowards(Hand.position, Position01.position, currentSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            while (currentSpeed > 0)
            {
                currentSpeed -= Time.deltaTime * Deceleration;
                if (currentSpeed > 0)
                {
                    Hand.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                }
                yield return new WaitForFixedUpdate();
            }

            currentSpeed = 0;
            while (!GeneralFunctions.CheckDestination(Hand.position, Position02.position, true, false, false, .1f))
            {
                if (currentSpeed < MaxVelocity)
                {
                    currentSpeed += Acceleration * Time.deltaTime;
                }
                if (currentSpeed > MaxVelocity)
                {
                    currentSpeed = MaxVelocity;
                }
                Hand.position = Vector3.MoveTowards(Hand.position, Position02.position, currentSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            currentSpeed *= -1;
            while (currentSpeed < 0)
            {
                currentSpeed += Time.deltaTime * Deceleration;
                if (currentSpeed < 0)
                {
                    Hand.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
    
}
