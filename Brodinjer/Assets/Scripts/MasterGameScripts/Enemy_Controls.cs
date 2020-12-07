using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy_Controls : MonoBehaviour
{
    private List<Enemy_Character_Manager> enemies;
    private List<Enemy_Manager> enemyManagers;

    public void KillAll()
    {
        enemies = FindObjectsOfType<Enemy_Character_Manager>().ToList();
        foreach (var enemy in enemies)
        {
            enemy.Character.Health.Death();
        }
    }

    public void FreezeAll()
    {
        enemyManagers = FindObjectsOfType<Enemy_Manager>().ToList();
        foreach (var enemy in enemyManagers)
        {
            enemy.Stun();
        }
    }
    
    public void UnFreezeAll()
    {
        enemyManagers = FindObjectsOfType<Enemy_Manager>().ToList();
        foreach (var enemy in enemyManagers)
        {
            enemy.UnStun();
        }
    }
}
