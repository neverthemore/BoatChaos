using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIBranch : MonoBehaviour
{
    //Синглтон
    //Вызывает пробоины и чинит
    [SerializeField] private GameObject BreachPoints;
    private Breach[] _allBreaches;

    [SerializeField] private Scrollbar _progressScrollbar;
    [SerializeField] private RectTransform _targetZone;
    //[SerializeField] private Image _background;

    [SerializeField, Range(1, 10)] private float _minSliderTime = 2f;  //Например время прохождение слайдера 5 секунд
    [SerializeField, Range(1, 10)] private float _maxSliderTime = 5f;

    [SerializeField] GameObject _BreachPanel;
    [SerializeField] TMP_Text _howMuchText;

    int _activeBreach = 0;

    private float _currentSliderTime;

    public static UIBranch Instance;

    bool _isActive = false;
    public bool IsActive => _isActive;

    private float _sliderValue;
    private float _targetPosition;

    private bool _success = false;

    public bool Success => _success;

    Vector2 _targetZonePosition;

    private void Awake()
    {
        Instance = this;
        _progressScrollbar.gameObject.SetActive(false);
    }

    private void Start()
    {
        _allBreaches = BreachPoints.GetComponentsInChildren<Breach>();

        _BreachPanel.gameObject.SetActive(false);
        //SpawnBreach();
    }

    private void Update()
    {
        if (_activeBreach > 0 )
        {
            _BreachPanel.gameObject.SetActive(true);
            _howMuchText.text = "Всего пробоин:" + _activeBreach.ToString();
        }
        else
        {
            _BreachPanel.gameObject.SetActive(false);
        }
        if (!_isActive) return;
        _sliderValue += Time.deltaTime / _currentSliderTime; //Заполняет шкалу за время
        UpdateUI();

        if (_sliderValue >= 1) CloseUI();
    }

    private void UpdateUI()
    {
        _progressScrollbar.value = _sliderValue;       //Значение слайдера применяется
        _targetZone.anchoredPosition = _targetZonePosition;
    }

    public void OpenUI()
    {
        //Показать панель и сделать логику с индикатором
        _progressScrollbar.gameObject.SetActive(true);
        ResetUI();
    }

    private void ResetUI()
    {
        _isActive = true;
        _sliderValue = 0f;
        _targetPosition = Random.Range(0.1f, 0.9f); //Позиция для целевой зоны
        _currentSliderTime = Random.Range(_minSliderTime, _maxSliderTime);

        Debug.Log("Позиция: "+ _targetPosition);
        _targetZonePosition = new Vector2(0 , -300 + 600 * _targetPosition);  //Координаты Зоны выставляем
    }

    public void CloseUI() //Закрывается если дошел до конца; или если нажали E -> когда закрылось нужно проверить где слайдер, если в зоне, то задание выполнено
    {
        _isActive = false;
        CheckZone();
        _progressScrollbar.gameObject.SetActive(false);
    }

    private void CheckZone()
    {
        float tolerance = 0.07f; // Допустимое отклонение
        _success = Mathf.Abs(_sliderValue - _targetPosition) <= tolerance;
        Debug.Log($"Остановка: {_sliderValue}, это {_success}");
    }

    public void FixBreach(GameObject breach)
    {
        breach.GetComponent<Breach>()?.DeactivateBreach();
        _activeBreach--;

    }

    public void SpawnBreach()
    {
        int _randomIndex = Random.Range(0, _allBreaches.Length);
        if (!_allBreaches[_randomIndex].IsActive)
        {
            _allBreaches[_randomIndex].ActivateBreach();
            _activeBreach++;
        }
    }
}
