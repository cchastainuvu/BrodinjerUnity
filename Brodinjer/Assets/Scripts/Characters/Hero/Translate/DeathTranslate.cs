using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Player/Translate/Death")]
public class DeathTranslate : CharacterTranslate
{
    
    private Vector3 _moveVec;
    private readonly WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();


    
    
    public override void Init(MonoBehaviour caller, CharacterController _cc, Transform camera, Z_Targeting target, Animator animator, RandomSoundController jump)
    {
        base.Init(caller, _cc, camera, target, animator, jump);
        base.Init(caller, _cc, camera, target, animator, jump);
        _moveVec = Vector3.zero;  
    }


    public override IEnumerator Move()
    {
        if(animation != null)
            animation.StartAnimation();
        while (true)
        {
                vSpeed -= Gravity * Time.deltaTime;
                _moveVec = Vector3.zero;
                _moveVec.y = vSpeed;
                _cc.Move(_moveVec * Time.deltaTime);

            yield return fixedUpdate;
        }

    }

    public override IEnumerator Run()
    {
        yield return null;
    }

    public override float getMoveAngle()
    {
        return 0;
    }

    public override float getSpeed()
    {
        return 0;
    }

       

}
