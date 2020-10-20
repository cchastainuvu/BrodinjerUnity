using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public PlayerMovement Movement;
    public Health_Base CharacterHealth;
    public LimitFloatData MagicAmount;

    public float GetCharacterHealth()
    {
        return CharacterHealth.GetHealth();
    }

    public float GetMagicAmount()
    {
        return MagicAmount.value;
    }

    public void ResetCharacter()
    {
        //Reset Character position and health
    }

    public void StopPlayer()
    {
        Movement.StopAll();
    }

    public void StartPlayer()
    {
        Movement.StartAll();
    }
    
}
