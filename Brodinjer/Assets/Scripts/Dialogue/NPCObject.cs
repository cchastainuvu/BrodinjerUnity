using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/NPC")]
public class NPCObject : ScriptableObject
{
    public Dialogue_Lines dialogue;

    public void SwapLines(Dialogue_Lines newLines)
    {
        dialogue = newLines;
    }
    
}
