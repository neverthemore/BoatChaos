using Unity.VisualScripting;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    [Header("Параметры корабля")]
    
    [SerializeField] private float _maxAngle;
    [SerializeField] private float turnSpeedMultiplier = 1f; // Множитель для скорости поворота корабля

    [Header("Объекты сцены")]
    [SerializeField] private Transform waterPivot;  // Точка у кормы корабля, используемая для расчёта поворота (дочерний объект корабля)
    //[SerializeField] private Transform waterObject; // Объект воды (дочерний объекта Waves), который движется в мировых координатах

    [Header("Параметры движения воды")]
    [SerializeField] private float _shipSpeed;


    GameObject _plane;
    private float _resultAngle;
    UIStatistic _UIStatistic;    
    [SerializeField] Wheel _wheel;
    void Start()
    {          
        _UIStatistic = GetComponent<UIStatistic>();
        _plane = GameObject.Find("Plane") ;        
    }



    void Update()
    {

        //Получаем угол штурвала и вычисляем результирующий угол (ограниченный maxAngle)
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);
        
        waterPivot.Rotate(0f, -_resultAngle * turnSpeedMultiplier * Time.deltaTime, 0f);
        
        //    Вне зависимости от поворота корабля, объект воды всегда движется вдоль мировых координат -Z.
        _plane.transform.Translate(Vector3.forward * -_shipSpeed * Time.deltaTime, Space.World);

        if (_UIStatistic.GameStart)
            _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }

}
