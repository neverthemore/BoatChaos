using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private string gameSceneName = "scene";

    [Header("Movement Settings")]
    [SerializeField] private GameObject objectToMove; // Объект для движения
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveDuration = 2f; // Время движения

    [Header("Fade Settings")]
    [SerializeField] private Image fadeImage; // Затемняющая картинка
    [SerializeField] private float fadeDuration = 1f; // Длительность затемнения

    [Header("Skybox Settings")]
    [SerializeField] private float rotationSpeed = 0.1f;

    private bool menuMovingShip = false;


    private Material skyboxMaterial;

    void Start()
    {
        // Инициализируем ссылку на материал skybox
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }

        // Скрываем панель настроек при старте
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void Update()
    {
        // Вращаем skybox
        if (skyboxMaterial != null)
        {
            float currentRotation = skyboxMaterial.GetFloat("_Rotation");
            skyboxMaterial.SetFloat("_Rotation", currentRotation + rotationSpeed * Time.deltaTime);
        }
    }

    
    public void OnPlayButtonClick()
    {
        if (!menuMovingShip)
        {
            StartCoroutine(StartGameSequence());
        }
    }

    private IEnumerator StartGameSequence()
    {
        menuMovingShip = true;

        // Запускаем движение объекта
        StartCoroutine(MoveObject());

        // Ждем окончания движения
        yield return new WaitForSeconds(fadeDuration);

        // Запускаем затемнение
        yield return StartCoroutine(FadeScreen());

        // Загружаем сцену
        SceneManager.LoadScene(gameSceneName);
    }
    private IEnumerator FadeScreen()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Плавное затемнение
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Гарантируем полное затемнение
        color.a = 1f;
        fadeImage.color = color;
    }
    private IEnumerator MoveObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            objectToMove.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public void OnSettingsButtonClick()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnExitButtonClick()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

  
   
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
   
}