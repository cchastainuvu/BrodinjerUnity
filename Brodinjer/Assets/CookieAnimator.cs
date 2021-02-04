using UnityEngine;

public class CookieAnimator : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private Light cookieLight;
    private static readonly int ShadowTex = Shader.PropertyToID("_ShadowTex");

    private void Start()
    {
        cookieLight = GetComponent<Light>();
        NextFrame();
        InvokeRepeating(nameof(NextFrame), 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        cookieLight.cookie = frames[frameIndex];
        if (frameIndex >= frames.Length - 1)
        {
            frameIndex = 0;
        }
        else
        {
            frameIndex++;
        }
    }
}
