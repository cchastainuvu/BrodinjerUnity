using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/Lines")]
public class Dialogue_Lines : ScriptableObject
{

    public string characterName;
    
    [TextArea(3, 10)]
    public List<string> lines;    

}
