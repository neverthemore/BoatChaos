using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{
    //������� ����� � ��������, ��������� �� �����
    //����� ����������������� �� ��������� (������)
    //����� ������� ����, ��� ���� ����� �������� ���������
    InputSystem_Actions inputActions;
    [SerializeField] ChoiceWheel _choiceWheel; //��������� ����������� ���� ���-��

    protected override void Update()
    {
        base.Update();

        RotateCamera();
        //���� ������ �������, �� ��������� UI(������� ���� ������), �� ���������� ��������� SwitchCharacter, � ������� ���� ������
        //� ������� ���� ������ Open � Close, ���� ������ 0, �� ������ �� ������?
    }

    protected override void RotateCamera()
    {
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        mouseX += look.x * Time.deltaTime * sensivity;
        mouseY -= look.y * Time.deltaTime * sensivity;        
        mouseX = Mathf.Clamp(mouseX, -80f, 80f);
        mouseY = Mathf.Clamp(mouseY, -75f, 75f);
        cmCamera.transform.localEulerAngles = new Vector3(mouseY, mouseX, 0f);
    }

    private void SwitchCharacter(int index)
    {
        CharacterManager.Instance.SwitchCharacter(index);
    }

    protected override void Start()
    {
        base.Start();
        //Activate();
    }

    public override void Activate()
    {
        base.Activate();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        //������ ������, �������� ����������
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
