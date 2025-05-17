using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainCharacter : BaseCharacter
{    
    [SerializeField] Wheel wheel;

    protected override void Update()
    {
        base.Update();
        RotateCamera();
        if (!_isActive || IsIll) return;
        if (inputActions.Captain.Drink.triggered)
        {
            Drink();
        }
    }
    protected override void RotateCamera()  //“ут что-то не работает
    {
        inputActions.Enable();
        base.RotateCamera();
        mouseX = Mathf.Clamp(mouseX, -75, 75);
        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, mouseX, 0f);              
    }
    protected override void Start()
    {
        base.Start();
        _itemTransform.gameObject.SetActive(false);
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
    public void SetUnActiveRum()
    {
        _itemTransform.gameObject.SetActive(false);
    }
    public void SetActiveRum()
    {
        _itemTransform.gameObject.SetActive(true);
    }
    protected void Drink()
    {        
        animator.SetTrigger("drinking");
    }
}
