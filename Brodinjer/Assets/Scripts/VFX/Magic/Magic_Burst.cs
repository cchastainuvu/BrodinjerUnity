using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magic_Burst : MonoBehaviour
{
    public GameObject BurstPrefab;
    public GameObject ProjectilePrefab;

    private Vector3 position;
    private Quaternion rotation;

    public UnityEvent onHit;

    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            position = ProjectilePrefab.transform.position;
            rotation = ProjectilePrefab.transform.rotation;
            ProjectilePrefab.SetActive(false);
            Instantiate(BurstPrefab, position, rotation).SetActive(true);
            onHit.Invoke();
        }
    }
}
