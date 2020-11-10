using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar_Fill : MonoBehaviour
{
    public LimitFloatData floatdata;
    public float updateDelay;
    public Image BarImage;
    private bool updating;
    private Coroutine updateFunc;
    private WaitForSeconds updateWait;
    public bool AwakeOnStart = true;

    private void Start()
    {
        updating = false;
        updateWait = new WaitForSeconds(updateDelay);
        if(AwakeOnStart)
            StartUpdate();
    }

    public void StartUpdate()
    {
        if (!updating)
        {
            updating = true;
            updateFunc = StartCoroutine(UpdateBar());
        }
    }

    private IEnumerator UpdateBar()
    {
        while (updating)
        {
            BarImage.fillAmount = floatdata.value / floatdata.MaxValue;
            yield return updateWait;
        }
    }

    public void StopUpdate()
    {
        if(updateFunc != null)
            StopCoroutine(updateFunc);
        updating = false;
    }
    
}
