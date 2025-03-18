using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    //�������, ����� ��������� � ������ � ������� �� ����

    protected override void Update()
    {
        base.Update();
        
        Move();

        //���� ������ ESC, �� ������� � ����       
        if (Keyboard.current.escapeKey.wasPressedThisFrame) SwitchCharacter();
    }

    private void Move()
    {
        //�������� ����� � ������� �����
    }

    private void SwitchCharacter()
    {
        CharacterManager.Instance.SwitchCharacter(0); //��������� � ��������
    }
}
