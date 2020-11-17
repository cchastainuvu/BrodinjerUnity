using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public BoolData gameIsPaused;
    private bool optionsMenuIsActive;
    public GameObject pauseMenuUI, optionsMenuUI;

    void Start()
    {
        optionsMenuIsActive = false;
        gameIsPaused.value = false;

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused.value)
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused.value = false;
    }

    public void Pause()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameIsPaused.value = true;
    }

    public void OptionsMenu()
    {
        //Debug.Log("Options Menu Loaded");
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        optionsMenuIsActive = true;
    }

    public void QuitGame()
    {
        //Debug.Log("Quit to Main Menu");
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameIsPaused.value = false;
        SceneManager.LoadScene(0);
    }
}
