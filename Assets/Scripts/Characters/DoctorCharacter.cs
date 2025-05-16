using UnityEngine;

public class DoctorCharacter : CrewCharacter
{    
    [SerializeField] private float _healRange;
    private BaseCharacter _currentHealCharacter;

    //protected override void StartIll()
    //{
    //    _isIll = false;
    //}

    protected override void Update()
    {
        base.Update();

        if (!_isActive) return;
        
        if (inputActions.Crew.Use.triggered) CastRayForHeal();
    }    

    private void CastRayForHeal()   //Попали в типа - вызвали его хилл
    {
        Ray ray = new Ray(cmCamera.transform.position, cmCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _healRange))
        {
            BaseCharacter _character = hit.collider.GetComponent<BaseCharacter>();
            if (_character != null)
            {
                if (_character.IsIll)
                {
                    _character.Cure();
                }
            }
        }
    }
}
