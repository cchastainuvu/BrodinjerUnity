using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    private bool optionsMenuIsActive;
    public GameObject pauseMenuUI, optionsMenuUI;

    void Start()
    {
        optionsMenuIsActive = false;
        GameIsPaused = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (optionsMenuIsActive)
                {
                    optionsMenuUI.SetActive(false);
                    pauseMenuUI.SetActive(true);
                }
                else
                {
                    Resume();    
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void OptionsMenu()
    {
        //Debug.Log("Options Menu Loaded");
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        //Debug.Log("Quit to Main Menu");
        SceneManager.LoadScene(0);
    }
}
