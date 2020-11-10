using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visible : MonoBehaviour
{
    public In_Camera_View CamRange;
    public Targeting TargetScript;
    public Transform player;
    private RaycastHit hit;
    private int layerMask;

    private void Start()
    {
        layerMask = ~LayerMask.GetMask("Enemy");
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
