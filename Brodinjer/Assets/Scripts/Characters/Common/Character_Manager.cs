using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character_Manager : MonoBehaviour
{
    public Character_Base Character;
    private Character_Base _characterTemp;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _characterTemp = Character.getClone();
        Character = _characterTemp;
        Character.Init(this, transform);
        if (GetComponent<Death_Event_Setup>() != null)
        {
            Death_Event death = Character.Health.Death_Version as Death_Event;
            if (death != null)
            {
                death._event = GetComponent<Death_Event_Setup>().DeathEvent;
            }
        }
    }

    public void TakeDamage(float amount, bool armor)
    {
        Character.Health.TakeDamage(amount, armor);
    }
    
}
