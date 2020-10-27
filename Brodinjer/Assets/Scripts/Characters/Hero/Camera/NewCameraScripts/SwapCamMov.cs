using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamMov : MonoBehaviour
{
    public GameObject cam1, cam2;
    public PlayerMovement movement;
    public CharacterTranslate translate1, translate2;
    public CharacterRotate rotate1, rotate2;

    public bool current;

    private void Start()
    {
        current = false;
    }

    public void Swap()
    {
        if (!current)
        {
            current = true;
            cam1.SetActive(false);
            cam2.SetActive(true);
            movement.SwapMovement(rotate2, translate2, null);
        }
        else
        {
            current = false;
            cam1.SetActive(true);
            cam2.SetActive(false);
            movement.SwapMovement(rotate1, translate1, null);
        }
    }
}
