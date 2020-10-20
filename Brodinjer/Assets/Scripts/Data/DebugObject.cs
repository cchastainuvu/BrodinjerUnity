using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Data/Debug")]
public class DebugObject : ScriptableObject
{
   public void DebugPrint(string statement)
   {
      Debug.Log(statement);
   }
}
