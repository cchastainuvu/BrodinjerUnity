using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    private Image barUI;
    public LimitFloatData MagicAmount;
    public float increaseSpeed;
    public bool autoRegen;
    public BoolData MagicUse;

    void Start()
    {
        barUI = GetComponent<Image>();
        MagicUse.value = false;
    }

    void FixedUpdate()
    {
        if (!MagicUse.value && autoRegen)
        {
            MagicAmount.AddFloat(increaseSpeed * Time.deltaTime);
        }
        
        barUI.fillAmount = MagicAmount.value / MagicAmount.MaxValue;
    }

    public void SetMagicRegen(float num)
    {
        increaseSpeed = num;
    }
}
