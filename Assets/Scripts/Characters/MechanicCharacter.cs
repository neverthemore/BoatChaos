using System.Drawing;
using UnityEngine;

public class MechanicCharacter : CrewCharacter
{
    [SerializeField] private float _fixRange = 2f;


    //Если в руках молоток, То мы можем нажать ЛКМ, который будет чинить сломанные объекты
    //Выпускает луч, который проверяет тег, пока он выпущен и наведен на цель то цель чинится (ну или логика внутри предмета)
    private float _secondsForFix = 1f;
    private float _clampedSeconds = 0f;

    private IFixable _currentFixable;

    protected override void Update()
    {
        if (!_isActive) return;
        base.Update();

        //Тут переделать логику, из-за того, что со всеми объектами Фикс разный - то и проверка должна быть внутри Фикса?

        if (inputActions.Crew.Attack.IsPressed() && GetItem()?.Name == "Hammer")  
        {
            if (CastRayForFixAndCheck())
            { 
                _clampedSeconds += Time.deltaTime;
                Debug.Log("Чиним");
            }
            else _clampedSeconds = 0;
        }
        else _clampedSeconds = 0f;

        if (_clampedSeconds >= _secondsForFix)
        {
            _currentFixable.StartFix();
            _clampedSeconds = 0f;
        }
        
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
