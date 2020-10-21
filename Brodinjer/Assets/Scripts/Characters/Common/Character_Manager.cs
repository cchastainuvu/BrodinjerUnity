using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character_Manager : MonoBehaviour
{
    public Character_Base Character;
    private Character_Base _characterTemp;
    public bool MainCharacter = false;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (!MainCharacter)
        {
            _characterTemp = Character.getClone();
            Character = _characterTemp;
        }
        Character.Init(this, transform, MainCharacter);
        if (GetComponent<Death_Event_Setup>() != null && Character.Health.Death_Version is Death_Event)
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
