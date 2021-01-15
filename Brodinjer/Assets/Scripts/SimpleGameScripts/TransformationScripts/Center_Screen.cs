using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_Screen : MonoBehaviour
{
    public Transform Camera;
    public float distance = 5.0f;
    private Quaternion rotation;
    private Vector3 position;

    private void FixedUpdate()
    {
        rotation = Camera.rotation;
        position = Camera.position + (rotation *Vector3.forward * distance);
        transform.position = position;
    }
}
