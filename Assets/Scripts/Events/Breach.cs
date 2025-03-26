using UnityEngine;

public class Breach : MonoBehaviour, IFixable
{
    [Header("MiniGame Settings")]
    [SerializeField] private float _sliderSpeed = 1f;
    [SerializeField, Range(0, 1)] private float _targetZoneSize = 0.3f;

    private bool _isFixed;
    private float _sliderValue;
    private float _targetPosition;
    private bool _isMiniGameActive;

    public float FixProgress => _sliderValue;
    public bool IsFixed => _isFixed;


    private void Update()
    {
        if (!_isMiniGameActive || _isFixed) return;

        // Движение ползунка
        _sliderValue += Time.deltaTime * _sliderSpeed;
        if (_sliderValue > 1f) _sliderValue = 0f;
    }


    public void StartFix()
    {
        if (!NeedToFix()) return;

        //Открываем панель UI
        ResetFix();
        TryFix();

    }

    public void ExitGame()
    {
        //Скрываем панель UI
        _isMiniGameActive = false;
    }

    public bool TryFix()
    {
        // Проверка попадания в зону
        bool isInZone = Mathf.Abs(_sliderValue - _targetPosition) < _targetZoneSize / 2;

        if (isInZone)
        {
            _isFixed = true;
            gameObject.SetActive(false);
        }

        return isInZone;
    }

    public void ResetFix()
    {
        _isMiniGameActive = true;
        _sliderValue = 0f;
        _targetPosition = Random.Range(0.1f, 0.9f); // Случайная позиция зоны
    }

    public bool NeedToFix()
    {
        if (_isFixed) return false;
        return true;
       
    }
}
