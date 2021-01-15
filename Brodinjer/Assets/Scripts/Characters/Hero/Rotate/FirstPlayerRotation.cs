using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Player/Rotate/FirstPersonRotate")]

public class FirstPlayerRotation : CharacterRotate
{
    
    private WaitForFixedUpdate _fixedUpdate;
    private Vector3 rotation;
    private Quaternion quat;

    public override void Init(Transform obj, Transform camera, Targeting target)
    {
        _fixedUpdate = new WaitForFixedUpdate();
        base.Init(obj, camera, target);
    }

    public override IEnumerator Rotate()
    {
        yield return new WaitForFixedUpdate();
    }
    
    
}
