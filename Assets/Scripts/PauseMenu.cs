using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu, volumeSlider;
    public Slider volumeControl;
    public StatePlayerController playerStatePlayerController;

    public static float volumeValue = 0.5f;
    public static bool isGamePaused = false;

    void Update() 
    {
        volumeValue = volumeControl.value;
        AudioListener.volume = volumeValue;
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused) {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused) {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        playerStatePlayerController.DisablePlayerControls();
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        playerStatePlayerController.EnablePlayerControls();
        isGamePaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        volumeSlider.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        Destroy(PlayerData.Instance);
        SceneManager.LoadScene(0);
    }

    public void GoToOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        volumeSlider.SetActive(true);
    }

    public void ReturnFromOptions()
    {
        optionsMenu.SetActive(false);
        volumeSlider.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
