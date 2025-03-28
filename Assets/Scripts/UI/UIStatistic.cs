using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatistic : MonoBehaviour
{
    public static UIStatistic Instance;

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
            Debug.Log("777 BIG WIN 777");
        }
        if (ShipHP <= 0)
        {
            Debug.Log("LOSE");
        }
        if (timeToLose <= 0)
        {
            Debug.Log("LOSEforTime");                                                                                                                                                                                              //����� 

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
        // ���������� ������� �������
        float normalizedValue = distanceSlider.normalizedValue;
        float targetX = normalizedValue * sliderWidth - (sliderWidth * 0.5f) + shipOffset;

        // ������� �������� �� X
        float smoothedX = Mathf.Lerp(
            shipIcon.anchoredPosition.x,
            targetX,
            Time.deltaTime * smoothSpeed
        );

        // �������� �������� �� Y
        float waveY = Mathf.Sin(Time.time * waveSpeed) * waveHeight;

        // ��������� ������� ���� ���
        shipIcon.anchoredPosition = new Vector2(
            smoothedX,
            initialShipY + waveY - shipYOffset // ���������� ��������� Y �������
        );
    }
}
