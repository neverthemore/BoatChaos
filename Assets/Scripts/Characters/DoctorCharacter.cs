using UnityEngine;

public class DoctorCharacter : CrewCharacter
{
    //Этот тип не может заболеть
    //Также лечит тиму

    protected override void StartIll()
    {
        _isIll = false;
    }

}
