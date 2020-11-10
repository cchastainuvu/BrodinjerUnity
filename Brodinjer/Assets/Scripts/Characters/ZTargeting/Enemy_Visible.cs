using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visible : MonoBehaviour
{
    private In_Camera_View CamRange;
    private Targeting TargetScript;
    private Transform player;
    private RaycastHit hit;
    private int layerMask;

    private void Awake()
    {
        layerMask = ~LayerMask.GetMask("Enemy");
        TargetScript = FindObjectOfType<Targeting>();
        CamRange = FindObjectOfType<In_Camera_View>();
        player = TargetScript.transform;
    }
    
    private void FixedUpdate()
    {
        CheckInRange();
    }

    public bool CheckInRange()
    {
        if (CamRange.InRange(transform))
        {
            if (!TargetScript.EnemiesInRange.Contains(gameObject))
            {
                if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit,
                    CamRange.MaxDistance, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        TargetScript.EnemiesInRange.Add(gameObject);
                    }
                }
            }
            return true;
        }

        else if (TargetScript.EnemiesInRange.Contains(gameObject))
        {
            TargetScript.EnemiesInRange.Remove(gameObject);
        }

        return false;
    }

    private void OnDisable()
    {
        TargetScript.EnemiesInRange.Remove(gameObject);
    }
}
