using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    //Команда, может двигаться и ходить в отличие от кэпа

    protected override void Update()
    {
        base.Update();
        
        Move();

        //Если нажали ESC, то возврат к кэпу       
        if (Keyboard.current.escapeKey.wasPressedThisFrame) SwitchCharacter();
    }

    private void Move()
    {
        //Получаем инпут и двигаем перса
    }

    private void SwitchCharacter()
    {
        CharacterManager.Instance.SwitchCharacter(0); //Вернулись к капитану
    }
}
