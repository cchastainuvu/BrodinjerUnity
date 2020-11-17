using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
   public void Load(string SceneName)
   {
      SceneManager.LoadScene(SceneName);
   }

   public void ReloadCurrent()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
    
    
}
