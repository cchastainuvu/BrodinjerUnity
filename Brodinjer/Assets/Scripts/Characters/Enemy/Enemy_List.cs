using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_List : MonoBehaviour
{
    public List<Enemy_Manager> GetEnemies()
    {
        return FindObjectsOfType<Enemy_Manager>().ToList();
    }

    public void StopEnemyAttack()
    {
        foreach (Enemy_Manager enemy in GetEnemies())
        {
            Debug.Log(enemy.gameObject.name);
            enemy.deactivateAttack();
        }
    }

    public void StopEnemyMove()
    {
        foreach (Enemy_Manager enemy in GetEnemies())
        {
            Debug.Log(enemy.gameObject.name);
            enemy.deactivateMove();
        }
    }
    
}
