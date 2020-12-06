using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling_VFX : MonoBehaviour
{
    public ParticleSystem Indicator, Highlighter;
    public GameObject ObjectToHighlight;

    private MeshFilter renderer;
    private Mesh mesh;
    private ParticleSystem.ShapeModule indicatorShape, highlightShape;

    private void Start()
    {
        indicatorShape = Indicator.shape;
        indicatorShape.enabled = true;
        highlightShape = Highlighter.shape;
        highlightShape.enabled = true;
        if (ObjectToHighlight != null && (renderer = ObjectToHighlight.GetComponent<MeshFilter>()) != null)
        {
            indicatorShape.shapeType = ParticleSystemShapeType.Mesh;
            indicatorShape.mesh = renderer.mesh;
            highlightShape.shapeType = ParticleSystemShapeType.Mesh;
            highlightShape.mesh = renderer.mesh;
        }
    }

    public void Highlight()
    {
        Debug.Log("Highlight");
        //Indicator.Stop();
        Highlighter.Play();
    }

    public void UnHighlight()
    {
        Debug.Log("UnHighlight");
        //Indicator.Play();
        Highlighter.Stop();
    }
}
