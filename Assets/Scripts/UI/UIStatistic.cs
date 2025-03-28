using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIStatistic : MonoBehaviour
{
    public static UIStatistic Instance;

    public GameOver _gameOver;

    [Header("UI Elements")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider distanceSlider;
    [SerializeField] private RectTransform shipIcon;
    [SerializeField] private float shipOffset = 30f;
    [SerializeField] private float smoothSpeed = 2f;
    [SerializeField] private float shipYOffset = 50f;
    [SerializeField] private float waveHeight = 5f;
    [SerializeField] private float waveSpeed = 2f;
    private RectTransform sliderRect;
    private float sliderWidth;

    [SerializeField] private TMP_Text timerText;

    [SerializeField] private float timeToLose = 720f;

    private float initialShipY;

    public bool GameStart = false;
    public float ShipHP;
    public float RemainingDistance;
    void Start()
    {
        Instance = this;
        RemainingDistance = 8500f;
        ShipHP = 100f;

        sliderRect = distanceSlider.GetComponent<RectTransform>();
        sliderWidth = sliderRect.rect.width;
        initialShipY = shipIcon.anchoredPosition.y;
        InitializeSliders();
        UpdateShipPosition();
        //_gameOver.OnGameOver?.Invoke();
    }
    
    void Update()
    {
        if (GameStart)
        {
            StartGame();
        }

        UpdateDisplay();
        InitializeSliders();
        UpdateShipPosition();
    }

    void UpdateDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeToLose / 60);
            int seconds = Mathf.FloorToInt(timeToLose % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void StartGame()
    {
        timeToLose -= Time.deltaTime;
        if (RemainingDistance <= 0)
        {
            _gameOver.OnGameVictory?.Invoke();
            Debug.Log("777 BIG WIN 777");
        }
        if (ShipHP <= 0)
        {
            Debug.Log("LOSE");
            _gameOver.OnGameOver?.Invoke();
        }
        if (timeToLose <= 0)
        {
            Debug.Log("LOSEforTime");
            _gameOver.OnGameOver?.Invoke(); 

        }
    }
    void InitializeSliders()
    {
        sliderRect = distanceSlider.GetComponent<RectTransform>();
        sliderWidth = sliderRect.rect.width;

        hpSlider.value = ShipHP;
      
        distanceSlider.value = 8500 - RemainingDistance;
    }
    void UpdateShipPosition()
    {
        // Вычисление целевой позиции
        float normalizedValue = distanceSlider.normalizedValue;
        float targetX = normalizedValue * sliderWidth - (sliderWidth * 0.5f) + shipOffset;

        // Плавное движение по X
        float smoothedX = Mathf.Lerp(
            shipIcon.anchoredPosition.x,
            targetX,
            Time.deltaTime * smoothSpeed
        );

        // Волновое движение по Y
        float waveY = Mathf.Sin(Time.time * waveSpeed) * waveHeight;

        // Обновляем позицию один раз
        shipIcon.anchoredPosition = new Vector2(
            smoothedX,
            initialShipY + waveY - shipYOffset // Используем начальную Y позицию
        );
    }
}
