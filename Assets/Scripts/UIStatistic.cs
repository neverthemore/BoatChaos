using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIStatistic : MonoBehaviour
{
    

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

    private float initialShipY;

    public float ShipHP;
    public float RemainingDistance;
    void Start()
    {
        RemainingDistance = 1000f;
        ShipHP = 100f;

        sliderRect = distanceSlider.GetComponent<RectTransform>();
        sliderWidth = sliderRect.rect.width;
        initialShipY = shipIcon.anchoredPosition.y;
        InitializeSliders();
        UpdateShipPosition();
    }
    
    void Update()
    {
        if (RemainingDistance <= 0)
        {
            Debug.Log("777 BIG WIN 777");
        }
        if (ShipHP <= 0)
        {
            Debug.Log("LOSE");
        }
      
        InitializeSliders();
        UpdateShipPosition();
    }


    void InitializeSliders()
    {
        sliderRect = distanceSlider.GetComponent<RectTransform>();
        sliderWidth = sliderRect.rect.width;

        hpSlider.value = ShipHP;
      
        distanceSlider.value = 1000 - RemainingDistance;
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
