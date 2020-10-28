using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Character/Common/Character")]
public class Character_Base : ScriptableObject
{
    public Health_Base Health;
    private Health_Base tempHealth;

    public virtual void Init(MonoBehaviour caller, Transform enemy, bool mainCharacter = false)
    {
        if (!mainCharacter)
        {
            tempHealth = Health.GetClone();
            Health = tempHealth;
        }

        Health.Init(caller, enemy, mainCharacter);
    }

    public virtual Character_Base getClone()
    {
        Character_Base temp = CreateInstance<Character_Base>();
        temp.Health = Health;
        return temp;
    }
    
}
