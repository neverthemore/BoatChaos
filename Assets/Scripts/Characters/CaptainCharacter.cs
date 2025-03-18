using UnityEngine;

public class CaptainCharacter : BaseCharacter
{
    //Капитан стоит у штурвала, двигаться не может
    //Может взаимодействовать со штурвалом (Максим)
    //Может вызвать круг, для того чтобы поменять персонажа

    [SerializeField] ChoiceWheel _choiceWheel; //Прокинуть зависимость надо как-то

    protected override void Update()
    {
        base.Update();
        //Если нажата клавиша, то открываем UI(сделать свой скрипт), по отпусканию запускаем SwitchCharacter, в который суем индекс
        //У скрипта тоже методы Open и Close, если индекс 0, то ничего не меняем?
    }

    private void SwitchCharacter(int index)
    {
        CharacterManager.Instance.SwitchCharacter(index);
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
