using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Follow : MonoBehaviour
{
    //public Transform LookAtObject;
    //private Quaternion facingDirection;
    public Transform FollowRotation;
    public Vector3 RotationOffset;
    private Vector3 eulerRotations;
    public Transform R_Brow, L_Brow;
    public Transform RBrowPosition, LBrowPosition;
    public bool Rotating;
    public List<Transform> destinations;

    private void LateUpdate()
    {
        /*if (Rotating)
        {
            /*facingDirection = Quaternion.LookRotation((LookAtObject.transform.position - transform.position).normalized);
            eulerRotations = facingDirection.eulerAngles;
            eulerRotations += RotationOffset;
            facingDirection = Quaternion.Euler(eulerRotations);
            transform.rotation = facingDirection;
            transform.rotation = FollowRotation.rotation;
            R_Brow.position = RBrowPosition.position;
            R_Brow.rotation = RBrowPosition.rotation;
            L_Brow.position = LBrowPosition.position;
            L_Brow.rotation = LBrowPosition.rotation;
        }*/
    }

    public void SetRotate(bool val)
    {
        Rotating = val;
    }
}
