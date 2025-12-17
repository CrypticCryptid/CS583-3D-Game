using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public string menuName;

    public GameObject settingMenuUI;
    public Slider senseSlider;
    MouseLook mouseLook;

    void Start()
    {
        mouseLook = FindObjectOfType<MouseLook>();

        senseSlider.value = mouseLook.GetSensitivity();
    }

    void Update()
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

        mouseLook.SetSensitivity(senseSlider.value);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplaySettings()
    {
        settingMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuName);
    }

    public void CloseTab(GameObject obj)
    {
        obj.SetActive(false);
    }

    // public void QuitGame()
    // {
    //     Debug.Log("Quitting Game...");
    //     Application.Quit();
    // }
}
