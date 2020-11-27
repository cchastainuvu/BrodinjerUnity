using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_Active : MonoBehaviour
{
   public GameObject obj;
   private bool active;

   private void Start()
   {
      active = obj.activeSelf;
   }

   public void Toggle()
   {
      obj.SetActive(!obj.activeSelf);

   }
}
