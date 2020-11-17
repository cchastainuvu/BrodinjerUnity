using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCheckpointObjects : MonoBehaviour
{
    public List<SavableScriptableObjects> SavedObjects;
    public List<SavableScriptableObjects> SceneObjects;

    public void Load()
    {
        if (SavedObjects.Count == SceneObjects.Count)
        {
            for (int i = 0; i < SavedObjects.Count; i++)
            {
                SceneObjects[i].SetObj(SavedObjects[i]);
            }
        }
    }

    public void Save()
    {
        if (SavedObjects.Count == SceneObjects.Count)
        {
            for (int i = 0; i < SceneObjects.Count; i++)
            {
                SavedObjects[i].SetObj(SceneObjects[i]);
            }
        }
    }
}
