using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/List/objects")]
public class ObjectList : ListBase<Object>
{
    public override void SetObj(ScriptableObject obj)
    {
        ObjectList temp = obj as ObjectList;

        if (temp != null)
        {
            list = temp.list;
        }
    }
}
