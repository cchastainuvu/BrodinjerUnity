using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public static class GeneralFunctions
{
    public static float ConvertRange(float origMinRange, float origMaxRange, float newMinRange, float newMaxRange, float value)
    {
        return (value - origMinRange) * (newMaxRange - newMinRange) / (origMaxRange - origMinRange) + newMinRange;
    }
    
    public static float GetDirection(Vector3 targetPosition, Vector3 mainPosition)
    {
        Vector3 collisionposition = targetPosition;
        collisionposition.y = 0;
        Vector3 transformposition = mainPosition;
        transformposition.y = 0;
        Vector3 target = collisionposition - transformposition;
        float angle = Vector3.Angle(target, mainPosition);
        Vector3 crossProduct = Vector3.Cross(target, mainPosition);
        if (crossProduct.y < 0)
        {
            angle = -angle;
        }
        return angle;
    }
}
