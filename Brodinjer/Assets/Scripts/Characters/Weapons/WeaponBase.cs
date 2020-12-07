using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public KeyCode WeaponNum;
    public Sprite WeaponSprite;
    protected IEnumerator attack;
    [HideInInspector] public bool currWeapon;
    [HideInInspector] public bool inUse;
    protected bool frozen;
    public Animator anim;
    public string AttackTrigger;
    public string AttackEndTrigger;
    private float animSpeed=1;
    public string useButton;
    public BoolData collected;
    protected Coroutine weaponFunc;
    public IntData NumItems;
    
    public abstract void Initialize();
    public abstract IEnumerator Attack();
    public abstract void End();

    public virtual void Freeze()
    {
        if (anim != null)
        {
            animSpeed = anim.speed;
            anim.speed = 0;
        }

        frozen = true;
    }

    public virtual void Unfreeze()
    {
        if(anim!= null)
            anim.speed = animSpeed;
        frozen = false;
    }
    
    

}
