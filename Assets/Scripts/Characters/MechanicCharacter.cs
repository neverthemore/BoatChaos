using System.Drawing;
using UnityEngine;

public class MechanicCharacter : CrewCharacter
{
    [SerializeField] private float _fixRange = 2f;

    // Добавляем переменную для отслеживания предыдущего состояния кнопки
    private bool _wasAttackPressedLastFrame = false;
    //Если в руках молоток, То мы можем нажать ЛКМ, который будет чинить сломанные объекты
    //Выпускает луч, который проверяет тег, пока он выпущен и наведен на цель то цель чинится (ну или логика внутри предмета)
    private float _secondsForFix = 1f;
    private float _clampedSeconds = 0f;


    private IFixable _currentFixable;

    protected override void Update()
    {
        base.Update();
        if (_isIll || !_isActive) return; //Перестает что-либо делать


        // Переменные для отслеживания состояния кнопки
        bool isAttackPressed = inputActions.Crew.Attack.IsPressed();
        bool isAttackTriggered = isAttackPressed && !_wasAttackPressedLastFrame;

        if (GetItem()?.Name == "Hammer")
        {
            bool isFixableHit = CastRayForFixAndCheck();

            if (isFixableHit)
            {
                // Обработка Wheel (удержание)
                if (_currentFixable is Wheel)
                {
                    if (isAttackPressed)
                    {
                        _clampedSeconds += Time.deltaTime;
                    }

                    // При достижении времени чиним
                    if (_clampedSeconds >= _secondsForFix)
                    {
                        _currentFixable.StartFix();
                        _clampedSeconds = 0f;
                    }
                }
                // Обработка ShipMast (одиночные нажатия)
                else if (_currentFixable is ShipMast)
                {
                    if (isAttackTriggered)
                    {
                        _currentFixable.StartFix();
                    }
                }
            }
            else
            {
                _clampedSeconds = 0f;
            }
        }
        else
        {
            _clampedSeconds = 0f;
        }

        // Обновляем состояние кнопки для следующего кадра
        _wasAttackPressedLastFrame = isAttackPressed;
        
    }

    private bool CastRayForFixAndCheck()   //Зажато ЛКМ, попало в IFixable -> вызывается StartFix()
    {
        Ray ray = new Ray(cmCamera.transform.position, cmCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _fixRange))
        {
            IFixable fixable = hit.collider.GetComponent<IFixable>();
            if (fixable != null)
            {
                if (fixable.NeedToFix())
                {
                    _currentFixable = fixable;
                    return true;
                }
            }
            else _currentFixable = null;
        }
        return false;
    }
}
