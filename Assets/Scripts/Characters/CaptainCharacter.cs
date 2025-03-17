using UnityEngine;

public class CaptainCharacter : BaseCharacter
{
    //Капитан стоит у штурвала, двигаться не может
    //Может взаимодействовать со штурвалом (Максим)
    //Может вызвать круг, для того чтобы поменять персонажа
    
    private void Update()
    {
        if (!_isActive) return; //Если не активный, то ничего не работает (вообще надо сделать, чтобы работал другой скрипт ИИ

        else
        {
            
        }
    }



    

    public override void Activate()
    {
        base.Activate();
        //Меняем камеру, включаем управление
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
