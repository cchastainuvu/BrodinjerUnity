using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float StartRotationSpeed;
    public float MaxRotationSpeed;
    public float SpeedIncrease;
    public float IncreaseTime;

    private float RotationSpeed;

    private void Start()
    {
        RotationSpeed = StartRotationSpeed;
        StartCoroutine(increaseSpeed());
    }

    private void FixedUpdate()
    {
         transform.Rotate(0,RotationSpeed*Time.deltaTime, 0);
    }

    private IEnumerator increaseSpeed()
    {
        while (RotationSpeed < MaxRotationSpeed)
        {
            yield return new WaitForSeconds(IncreaseTime);
            RotationSpeed += SpeedIncrease;
        }
    }
}
