using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Start_Camera_Shake : MonoBehaviour
{

    private bool shaking;
    public float shakeAmp = 3, shakeFreq = 1, ShakeStartTime = .5f, ShakeEndTime = .5f;
    public CinemachineVirtualCamera VC;
    private CinemachineBasicMultiChannelPerlin shake;

    private void Start()
    {
        shake = VC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shaking = false;
    }
    public void StartShake()
    {
        if (!shaking)
        {
            shaking = true;
            StartCoroutine(ShakeChange(0, shakeAmp, 0, shakeFreq, ShakeStartTime));
        }
    }

    private IEnumerator ShakeChange(float startamp, float endamp, float startfreq, float endfreq, float changetime)
    {
        float curtime = 0;
        while (curtime < changetime)
        {
            curtime += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(startamp, endamp, GeneralFunctions.ConvertRange(0, changetime, 0, 1, curtime));
            shake.m_FrequencyGain = Mathf.Lerp(startfreq, endfreq, GeneralFunctions.ConvertRange(0, changetime, 0, 1, curtime));
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopShake()
    {
        if (shaking)
        {
            shaking = false;
            StartCoroutine(ShakeChange(shakeAmp, 0, shakeFreq, 0, ShakeEndTime));
        }
    }
}
