using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public float pauseDilation = .01f;
    [SerializeField]
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = pauseDilation;
        GameIsPaused = true;
    }

    public void SettingsMenu()
    {
        Debug.Log("opening settings panel...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
