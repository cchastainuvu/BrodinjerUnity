using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public GameObject target;

    private void Awake()
    {
        target.SetActive(false);
    }
}
