using UnityEngine;

public class CaptainCharacter : BaseCharacter
{
    //������� ����� � ��������, ��������� �� �����
    //����� ����������������� �� ��������� (������)
    //����� ������� ����, ��� ���� ����� �������� ���������

    private void Update()
    {
        if (!_isActive) return; //���� �� ��������, �� ������ �� �������� (������ ���� �������, ����� ������� ������ ������ ��


    }



    

    public override void Activate()
    {
        base.Activate();
        //������ ������, �������� ����������
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
