using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    public Transform ObjToMove;
    private CharacterController _cc;

    private void Awake()
    {
        _cc = ObjToMove.GetComponent<CharacterController>();
    }

    public void Move(Transform PlaceToMoveTo)
    {
        if (_cc != null)
            _cc.enabled = false;
        ObjToMove.transform.position = PlaceToMoveTo.transform.position;
        ObjToMove.rotation = PlaceToMoveTo.rotation;
        if (_cc != null)
            _cc.enabled = true;
    }
}
