using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexWeight
{
    public int index;
    public Vector3 localPosition;
    public float weight;

    public VertexWeight(int i, Vector3 p, float w)
    {
        index = i;
        localPosition = p;
        weight = w;
    }
}
