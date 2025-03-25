using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{
    //������� ����� � ��������, ��������� �� �����    
    [SerializeField] Wheel wheel;   

    protected override void Update()
    {
        if (_isIll) return;

        if (!_isActive) return;
        base.Update();
        RotateCamera();
        
    }

    protected override void RotateCamera()  //��� ���-�� �� ��������
    {
        base.RotateCamera();
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, mouseX, 0f);
    }

    private void SwitchCharacter(int index) //����� ����� �� ������������
    {
        CharacterManager.Instance.SwitchCharacter(index);
    }

    protected override void Start()
    {
        base.Start();        
    }

    public override void Activate()
    {
        base.Activate();
        wheel.SetRotation(true);        
    }

    public override void Deactivate()
    {
        base.Deactivate();
        wheel.SetRotation(false);        
    }
}
