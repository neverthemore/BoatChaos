using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    protected override void Update()
    {
        if (_isIll) return;

        base.Update();

        if (!_isActive) return;

    }
}
