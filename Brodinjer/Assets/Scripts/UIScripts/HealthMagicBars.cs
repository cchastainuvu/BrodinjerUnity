using UnityEngine;
using UnityEngine.UI;

public class HealthMagicBars : MonoBehaviour
{
    private Image HealthMagicBar;
    public float CurrentValue, MaxValue;
    
    void Start()
    {
        HealthMagicBar = GetComponent<Image>();
    }

    void Update()
    {
        HealthMagicBar.fillAmount = CurrentValue / MaxValue;
    }
}
