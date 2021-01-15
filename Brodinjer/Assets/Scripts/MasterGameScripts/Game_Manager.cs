using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public PlayerMovement Movement;
    public LimitFloatData CharacterHealth;
    public LimitFloatData MagicAmount;

    public float GetCharacterHealth()
    {
        return CharacterHealth.value;
    }

    public float GetCharacterHealthPercentage()
    {
        return CharacterHealth.value / CharacterHealth.MaxValue;
    }

    public float GetMagicAmount()
    {
        return MagicAmount.value;
    }

    public float GetMagicPercentage()
    {
        return MagicAmount.value / MagicAmount.MaxValue;
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
