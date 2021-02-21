using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScalableObjectBase : MonoBehaviour
{
    public Scaling_VFX highlightFX;
    public SoundController growSound;

    public abstract void SetInit(float scale);
    public abstract void SetMax();
    public abstract bool ScaleUp(bool deltaTimed);
    public abstract bool ScaleDown(bool deltaTimed);
    public abstract void AutoScale(float time, bool Up);
}
