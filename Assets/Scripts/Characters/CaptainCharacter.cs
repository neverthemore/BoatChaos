using UnityEngine;

public class CaptainCharacter : BaseCharacter
{
    //������� ����� � ��������, ��������� �� �����
    //����� ����������������� �� ��������� (������)
    //����� ������� ����, ��� ���� ����� �������� ���������

    [SerializeField] ChoiceWheel _choiceWheel; //��������� ����������� ���� ���-��

    protected override void Update()
    {
        base.Update();
        //���� ������ �������, �� ��������� UI(������� ���� ������), �� ���������� ��������� SwitchCharacter, � ������� ���� ������
        //� ������� ���� ������ Open � Close, ���� ������ 0, �� ������ �� ������?
    }

    private void SwitchCharacter(int index)
    {
        CharacterManager.Instance.SwitchCharacter(index);
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
