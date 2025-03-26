using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private float _smoothTime = 2f; // Время для выравнивания

    private Vector3 _velocity = Vector3.zero;

    private bool _needToFloat = false;

    private void Update()
    {
        FloatToPoint();
    }

    public void SetOffset(Vector3 point)  //Куда плыть кораблю
    {
        _offset = point;
        _needToFloat = true;
    }

    //Надо скрипт отвечающий за выстрелы и попадания ядра

    private void FloatToPoint() //Плывет к точке если надо
    {
        if (_needToFloat)
        {
            Vector3 targetPosition = _offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _velocity,
                _smoothTime
            );

            if (transform.position == _offset) _needToFloat = false;
        }
    }
}
