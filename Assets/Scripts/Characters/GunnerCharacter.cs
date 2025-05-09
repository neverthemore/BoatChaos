using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    protected override void Update()
    {
        base.Update();
        if (!_isActive)
        {
            animator.SetBool("use", false);
            return; //Перестает что-либо делать
        }       

    }
}
