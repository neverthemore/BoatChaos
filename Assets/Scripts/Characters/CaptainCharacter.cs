using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{
    //������� ����� � ��������, ��������� �� �����    
    [SerializeField] Wheel wheel;   

    protected override void Update()
    {
        base.Update();
        RotateCamera();
        if (!_isActive) return;           
    }
    protected override void RotateCamera()  //��� ���-�� �� ��������
    {
        inputActions.Enable();
        base.RotateCamera();
        mouseX = Mathf.Clamp(mouseX, -75, 75);
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, mouseX, 0f);              
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
