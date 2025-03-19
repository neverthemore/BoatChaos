using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    CharacterController controller;
    InputSystem_Actions inputActions;
    float _speedOfMoving = 5f;
    //Команда, может двигаться и ходить в отличие от кэпа

    override protected void Start()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        cmCameraGameObject = GameObject.Find("CM Camera" + _characterName);
    }
    protected override void RotateCamera()
    {
        //Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        //gameObject.transform.Rotate(new Vector3(look.y, look.x, 0f));
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        mouseX += look.x * Time.deltaTime * sensivity;
        mouseY -= look.y * Time.deltaTime * sensivity;        
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, mouseX, 0f);
    }

    protected override void Update()
    {
        base.Update();
        
        Move();

        RotateCamera();

        //Если нажали ESC, то возврат к кэпу       //сделал через инпут систему
        if (inputActions.Crew.Leave.IsPressed()) 
            SwitchCharacter();
    }  
    private void Move()
    {
        //Получаем инпут и двигаем перса //прописал мув
        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();        
        Vector3 move = new Vector3();
        move = transform.forward * direction.y + transform.right * direction.x;
        move *= _speedOfMoving * Time.deltaTime;
        controller.Move(move);  
    }

    private void SwitchCharacter()
    {
        CharacterManager.Instance.SwitchCharacter(0); //Вернулись к капитану
    }
}
