using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    CharacterController controller;
    InputSystem_Actions inputActions;
    float _speedOfMoving = 5f;
    //�������, ����� ��������� � ������ � ������� �� ����

    override protected void Start()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new InputSystem_Actions();
    }
    protected override void RotateCamera()
    {
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        cmCameraGameObject.transform.Rotate(new Vector3(look.y, look.x, 0f));
    }

    protected override void Update()
    {
        base.Update();
        
        Move();

        //���� ������ ESC, �� ������� � ����       //������ ����� ����� �������
        if (inputActions.Crew.Leave.IsPressed()) 
            SwitchCharacter();
    }

    private void Move()
    {
        //�������� ����� � ������� ����� //�������� ���
        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(direction.y, 0f, direction.x);
        move *= _speedOfMoving;
        controller.Move(move);        
    }

    private void SwitchCharacter()
    {
        CharacterManager.Instance.SwitchCharacter(0); //��������� � ��������
    }
}
