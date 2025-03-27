using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private string mainMenuScene = "MainMenu";

    private BaseCharacter currentCharacter;
    private bool isPaused = false;

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

    private void FindActiveCharacter()
    {
        foreach (BaseCharacter character in FindObjectsOfType<BaseCharacter>())
        {
            if (character._isActive)
            {
                currentCharacter = character;
                break;
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindActiveCharacter();

        // Получаем текущие значения
        volumeSlider.value = AudioListener.volume;

        if (currentCharacter != null)
            sensitivitySlider.value = currentCharacter.Sensitivity;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetSensitivity(float sensitivity)
    {
        if (currentCharacter != null)
            currentCharacter.Sensitivity = sensitivity;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}