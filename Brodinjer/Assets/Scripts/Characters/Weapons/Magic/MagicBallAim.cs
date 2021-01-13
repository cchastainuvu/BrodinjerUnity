using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallAim : MonoBehaviour
{
   public Transform PositionPlacement;
   public Transform LookatObj;

   private void Start()
   {
      transform.position = PositionPlacement.position;
      transform.LookAt(LookatObj);
   }
}
