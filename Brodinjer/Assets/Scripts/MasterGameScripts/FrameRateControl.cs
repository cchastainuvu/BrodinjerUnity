using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateControl : MonoBehaviour
{
    public int frameCount = 60;
    
    private void Start()
    {
        Application.targetFrameRate = frameCount;
    }
}
