using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class SettingsManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button applyButton;

    private List<Resolution> filteredResolutions;
    private bool settingsChanged = false;

    void Start()
    {
        InitializeUIComponents();
        LoadSettings();
        SetupEventListeners();
    }

    void InitializeUIComponents()
    {
        // Инициализация разрешений
        resolutionDropdown.ClearOptions();
        filteredResolutions = GetFilteredResolutions();

        List<string> options = new List<string>();
        foreach (var res in filteredResolutions)
        {
            options.Add($"{res.width}x{res.height}");
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GetCurrentResolutionIndex();

        // Инициализация качества графики
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        // Инициализация громкости
        volumeSlider.value = AudioListener.volume;
    }

    List<Resolution> GetFilteredResolutions()
    {
        // Фильтрация и сортировка разрешений
        List<Resolution> uniqueRes = new List<Resolution>();
        foreach (Resolution res in Screen.resolutions)
        {
            if (!uniqueRes.Exists(r => r.width == res.width && r.height == res.height))
            {
                uniqueRes.Add(res);
            }
        }
        uniqueRes.Sort((a, b) => b.width.CompareTo(a.width));
        return uniqueRes;
    }

    int GetCurrentResolutionIndex()
    {
        // Поиск текущего разрешения в списке
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            if (filteredResolutions[i].width == Screen.currentResolution.width &&
                filteredResolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return filteredResolutions.Count - 1;
    }

    void SetupEventListeners()
    {
        // Динамическое обновление настроек
        fullscreenToggle.onValueChanged.AddListener(delegate {
            settingsChanged = true;
            Screen.fullScreen = fullscreenToggle.isOn;
        });

        resolutionDropdown.onValueChanged.AddListener(delegate {
            settingsChanged = true;
            SetResolution(resolutionDropdown.value);
        });

        qualityDropdown.onValueChanged.AddListener(delegate {
            settingsChanged = true;
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        });

        volumeSlider.onValueChanged.AddListener(delegate {
            settingsChanged = true;
            AudioListener.volume = volumeSlider.value;
        });

        applyButton.onClick.AddListener(SaveSettings);
    }

    void SetResolution(int index)
    {
        Resolution selected = filteredResolutions[index];
        Screen.SetResolution(selected.width, selected.height, Screen.fullScreen);
    }

    void SaveSettings()
    {
        if (!settingsChanged) return;

        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("QualityIndex", qualityDropdown.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();

        settingsChanged = false;
        Debug.Log($"Сохранение настроек: Fullscreen: { fullscreenToggle.isOn} Resolution: { resolutionDropdown.value} Quality: { qualityDropdown.value}Volume: { volumeSlider.value}");
        Debug.Log("Настройки успешно сохранены!");
    }

    void LoadSettings()
    {
        // Загрузка с проверкой валидности значений
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        int resIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);
        resolutionDropdown.value = (resIndex >= 0 && resIndex < filteredResolutions.Count)
            ? resIndex
            : GetCurrentResolutionIndex();

        qualityDropdown.value = PlayerPrefs.GetInt("QualityIndex", QualitySettings.GetQualityLevel());
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", AudioListener.volume);

        // Принудительное применение настроек
        ApplyCurrentSettings();
    }

    void ApplyCurrentSettings()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        SetResolution(resolutionDropdown.value);
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        AudioListener.volume = volumeSlider.value;
    }

    void OnApplicationQuit()
    {
        if (settingsChanged) SaveSettings();
    }
}