using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{
    //Капитан стоит у штурвала, двигаться не может
    //Может взаимодействовать со штурвалом (Максим)
    //Может вызвать круг, для того чтобы поменять персонажа
    InputSystem_Actions inputActions;
    [SerializeField] Wheel wheel;
    //[SerializeField] ChoiceWheel _choiceWheel; //Прокинуть зависимость надо как-то

    protected override void Update()
    {
        base.Update();

        RotateCamera();
        //Если нажата клавиша, то открываем UI(сделать свой скрипт), по отпусканию запускаем SwitchCharacter, в который суем индекс
        //У скрипта тоже методы Open и Close, если индекс 0, то ничего не меняем? -Максим: бля я ебал создам попозже другой скрипт я пока чисто через manager UIум управляю
    }

    protected override void RotateCamera()
    {
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        mouseX += look.x * Time.deltaTime * sensivity;
        mouseY -= look.y * Time.deltaTime * sensivity;        
        mouseX = Mathf.Clamp(mouseX, -80f, 80f);
        mouseY = Mathf.Clamp(mouseY, -75f, 75f);
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, mouseX, 0f);
    }

    private void SwitchCharacter(int index)
    {
        CharacterManager.Instance.SwitchCharacter(index);
    }

    protected override void Start()
    {
        base.Start();
        //wheel = GetComponent<Wheel>();
        inputActions = new InputSystem_Actions();
    }

    public override void Activate()
    {
        base.Activate();
        wheel.SetRotation(true);
        //inputActions.Enable();
        //Меняем камеру, включаем управление
    }

    public override void Deactivate()
    {
        base.Deactivate();
        wheel.SetRotation(false);
        //inputActions.Disable();
    }
}
