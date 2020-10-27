using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_01 : MonoBehaviour
{
    public float minCamAngle, maxCamAngle;
    private float mouseX, mouseY;
    public string CameraHorizontal, CameraVertical;
    public float rotationSpeed;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX += Input.GetAxis(CameraHorizontal) * rotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis(CameraVertical) * rotationSpeed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, minCamAngle, maxCamAngle);
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);   
    }

}
