using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NUnit.Framework;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private string mainMenuScene = "MainMenu";

    private BaseCharacter currentCharacter;
    private bool isPaused = false;

    public static float MouseSense; //Публичное статичное поле

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
        // Получаем текущие значения
               
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        volumeSlider.value = AudioListener.volume;
        sensitivitySlider.value = MouseSense;
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        //AudioListener.volume = volume;
    }

    public void SetSensitivity()
    {
        MouseSense = sensitivitySlider.value;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}