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

    private void Moving()
    {
        Vector3 move = transform.forward * _shipSpeed * Time.deltaTime * -1;
        _plane.transform.localPosition += move;
        _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }

    private void Rotating()
    {
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp((angle / (1080 / _maxAngle)), -_maxAngle, _maxAngle);        
        transform.eulerAngles = new Vector3(0f, -_resultAngle, 0f);
    }


    void Update()
    {
        //Moving();
        //Rotating();

        //Получаем угол штурвала и вычисляем результирующий угол (ограниченный maxAngle)
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);
        
        waterPivot.Rotate(0f, _resultAngle * turnSpeedMultiplier * Time.deltaTime, 0f);
        
        //    Вне зависимости от поворота корабля, объект воды всегда движется вдоль мировых координат -Z.
        _plane.transform.Translate(Vector3.forward * -_shipSpeed * Time.deltaTime, Space.World);
    }

    void MbMOve()
    {
        // Поворот точки у кормы корабля (waterPivot) на основе угла штурвала
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp((angle / (1080 / _maxAngle)), -_maxAngle, _maxAngle);
        waterPivot.eulerAngles = new Vector3(0f, -_resultAngle, 0f);

        // Получаем направление движения корабля через поворот Pivot-а
        Vector3 shipForward = waterPivot.forward;
        // Инвертируем, чтобы вода двигалась против направления корабля
        Vector3 waterDirection = -shipForward;

        // Перемещаем объект воды
        transform.position += waterDirection * _shipSpeed * Time.deltaTime;
    }

    void NotWork()
    {
        // Получаем текущий угол штурвала и вычисляем результирующий угол,
        // ограниченный значением _maxAngle.
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);

        // Если штурвал не в центре, корабль должен поворачивать.
        // Поворот происходит относительно оси Y с учетом множителя скорости.
        if (Mathf.Abs(_resultAngle) > 0.01f)
        {
            transform.Rotate(0f, _resultAngle * turnSpeedMultiplier * Time.deltaTime, 0f);
        }

        // Обновляем локальный поворот waterPivot, чтобы он отражал угол штурвала.
        // Он может находиться, например, в точке у кормы корабля.
        waterPivot.localRotation = Quaternion.Euler(0f, -_resultAngle, 0f);

        // Для вычисления направления движения корабля берём глобальное направление waterPivot.forward.
        // Инвертируем его, чтобы вода двигалась против направления движения корабля.
        Vector3 shipForward = waterPivot.forward;
        Vector3 waterDirection = -shipForward;

        // Смещаем объект воды (например, Plane) в направлении waterDirection.
        // Таким образом создается иллюзия, что корабль движется, а вода движется в обратную сторону.
        _plane.transform.localPosition += waterDirection * _shipSpeed * Time.deltaTime;

        // Обновляем статистику, если это необходимо.
        _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }
}
