using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flickering_Light : MonoBehaviour
{
    private bool burning;
    public Light FlickerLight;

    public bool FlickerIntensity;
    public float minIntensity, maxIntensity;
    public float minIntensityTime, maxIntensityTime;
    private float intensityTime, intensityScale, origIntensity, newIntensity;

    public bool FlickerMovement;
    public Vector3 maximumMoveAmount;
    private Vector3 newMove, origMove, minimumVector, maximumVector;
    public float minMoveTime, maxMoveTime;
    private float newMoveX, newMoveY, newMoveZ, origMoveX, origMoveY, origMoveZ;
    private float moveTime, moveScale;

    public bool FlickerColor;
    public Color color01 = Color.white, color02 = Color.white;
    private Color newColor, origColor;
    public float minColorTime, maxColorTime;
    private float colortime, colorScale;

    private Coroutine intenstityFunc, moveFunc, colorFunc;

    private void Start()
    {
        if (FlickerLight == null)
            FlickerLight = GetComponent<Light>();
        StartFlicker();
    }

    public void StartFlicker()
    {
        burning = true;
        if (FlickerIntensity)
            intenstityFunc = StartCoroutine(IntensityFlicker());
        if (FlickerMovement)
            moveFunc = StartCoroutine(MoveFlicker());
        if (FlickerColor)
            colorFunc = StartCoroutine(ColorFlicker());
    }

    private IEnumerator MoveFlicker()
    {
        minimumVector = FlickerLight.transform.position - maximumMoveAmount;
        maximumVector = FlickerLight.transform.position + maximumMoveAmount;
        while (burning)
        {
            moveTime = Random.Range(minMoveTime, maxMoveTime);
            newMove = new Vector3(Random.Range(minimumVector.x, maximumVector.x),
                Random.Range(minimumVector.y, maximumVector.y),
                Random.Range(minimumVector.z, maximumVector.z)); ;
            origMove = FlickerLight.transform.position;
            moveScale = 0;
            float currentTime = 0;
            while (currentTime < moveTime)
            {
                currentTime += Time.deltaTime;
                moveScale = currentTime / moveTime;
                FlickerLight.transform.position = Vector3.Lerp(origMove, newMove, moveScale);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator IntensityFlicker()
    {
        while (burning)
        {
            intensityTime = Random.Range(minIntensityTime, maxIntensityTime);
            newIntensity = Random.Range(minIntensity, maxIntensity);
            origIntensity = FlickerLight.intensity;
            intensityScale = 0;
            float currentTime = 0;
            while (currentTime < intensityTime)
            {
                currentTime += Time.deltaTime;
                intensityScale = currentTime / intensityTime;
                FlickerLight.intensity = Mathf.Lerp(origIntensity, newIntensity, intensityScale);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator ColorFlicker()
    {
        while (burning)
        {
            colortime = Random.Range(minColorTime, maxColorTime);
            newColor = new Color(Random.Range(color01.r, color02.r),
                Random.Range(color01.g, color02.g),
                Random.Range(color01.b, color02.b),
                Random.Range(color01.a, color02.a));
            origColor = FlickerLight.color;
            colorScale = 0;
            float currentTime = 0;
            while (currentTime < colortime)
            {
                currentTime += Time.deltaTime;
                colorScale = currentTime / colortime;
                FlickerLight.color = Color.Lerp(origColor, newColor, colorScale);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
    }


    public void StopFlicker()
    {
        burning = false;
        if (intenstityFunc != null)
        {
            StopCoroutine(intenstityFunc);
        }
        if (moveFunc != null)
        {
            StopCoroutine(moveFunc);
        }
        if (colorFunc != null)
        {
            StopCoroutine(colorFunc);
        }
    }
}
