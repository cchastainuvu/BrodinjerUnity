using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Death_Base : ScriptableObject
{
    protected Transform character;
    
    public virtual void Init(Transform character)
    {
        this.character = character;
    }
    
    public abstract IEnumerator Death();

    public abstract Death_Base GetClone();
}
