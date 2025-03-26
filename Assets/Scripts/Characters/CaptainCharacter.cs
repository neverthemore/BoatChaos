using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{
    //Капитан стоит у штурвала, двигаться не может    
    [SerializeField] Wheel wheel;   

    protected override void Update()
    {
        base.Update();
        if (_isIll || !_isActive) return;
        RotateCamera();
        
    }

    protected override void RotateCamera()  //Тут что-то не работает
    {
        base.RotateCamera();
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, mouseX, 0f);
    }

    private void SwitchCharacter(int index) //Вроде метод не используется
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
