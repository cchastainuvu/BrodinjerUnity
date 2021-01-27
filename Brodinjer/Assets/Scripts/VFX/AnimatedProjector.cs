using UnityEngine;

public class AnimatedProjector : MonoBehaviour {

    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private Projector projector;
    private static readonly int ShadowTex = Shader.PropertyToID("_ShadowTex");

    private void Start()
    {
        projector = GetComponent<Projector>();
        NextFrame();
        InvokeRepeating(nameof(NextFrame), 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        projector.material.SetTexture(ShadowTex, frames[frameIndex]);
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
