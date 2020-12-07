using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Hand_Magic_Attack : Enemy_Attack_Base
{
    private GameObject magicObj;
    public float ForwardVelocity;
    private Rigidbody rigid;
    private Vector3 forcedirection;
    
    public override IEnumerator Attack()
    {
        attacking = true;
        if(animations != null)
            animations.StartAnimation();
        yield return new WaitForSeconds(AttackStartTime);
        magicObj = Instantiate(WeaponAttackobj, WeaponAttackobj.transform);
        magicObj.SetActive(true);
        rigid = magicObj.GetComponent<Rigidbody>();
        if (!rigid)
            rigid = WeaponAttackobj.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        magicObj.transform.parent = null;
        magicObj.transform.localScale = WeaponAttackobj.transform.lossyScale;
        forcedirection = WeaponAttackobj.transform.forward * ForwardVelocity;
        rigid.AddForce(forcedirection, ForceMode.Impulse);
        yield return new WaitForSeconds(AttackActiveTime);
        if(animations!= null)
            animations.StopAnimation();
        yield return new WaitForSeconds(CoolDownTime);
        attacking = false;
    }

    /*public override Enemy_Attack_Base getClone()
    {
        Hand_Magic_Attack temp = CreateInstance<Hand_Magic_Attack>();
        temp.ForwardVelocity = ForwardVelocity;
        temp.AttackActiveTime = AttackActiveTime;
        temp.CoolDownTime = CoolDownTime;
        temp.AttackStartTime = AttackStartTime;
        temp.animations = animations;
        temp.attackWhileMoving = attackWhileMoving;

        return temp;
    }*/
}



                    


