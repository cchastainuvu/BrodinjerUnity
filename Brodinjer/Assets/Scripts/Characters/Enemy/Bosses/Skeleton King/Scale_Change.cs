using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Change : MonoBehaviour
{
    public Vector3 minScale, maxScale;
    public float scaleTime;
    public Transform obj;

    public void ScaleUp()
    {
        StartCoroutine(Scale(minScale, maxScale));
    }

    private IEnumerator Scale(Vector3 origScale, Vector3 newScale)
    {
        float currentTime = 0;
        while (currentTime < scaleTime)
        {
            currentTime += Time.deltaTime;
            obj.localScale = Vector3.Lerp(origScale, newScale, GeneralFunctions.ConvertRange(0, scaleTime, 0, 1, currentTime));
            yield return new WaitForFixedUpdate();
        }
    }

}
