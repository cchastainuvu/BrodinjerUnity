using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emissive_Change : MonoBehaviour
{
    private Material MAT;
    public float ChangeTime;
    public Color InitColor, EmissiveColor;

    private float currentTime;
    private bool changing;

    private void Start()
    {
        MAT = GetComponent<Renderer>().material;
        MAT.SetColor("_EmissionColor", InitColor);
    }

    public void StartGlow()
    {
        if (!changing)
        {
            changing = true;
            StartCoroutine(Change(InitColor, EmissiveColor));
        }
    }

    private IEnumerator Change(Color origColor, Color newColor)
    {
        currentTime = 0;
        while (currentTime < ChangeTime && changing)
        {
            currentTime += Time.deltaTime;
            MAT.SetColor("_EmissionColor", Color.Lerp(origColor, newColor, GeneralFunctions.ConvertRange(0,ChangeTime, 0,1, currentTime)));
            yield return new WaitForFixedUpdate();
        }
        changing = false;
    }
}
