using System.Data;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseCharacter : MonoBehaviour
{    
    [SerializeField] protected string _characterName;
    protected GameObject cmCameraGameObject;
    [SerializeField] protected CinemachineCamera cmCamera; 
    protected InputSystem_Actions inputActions;

    protected float mouseX;
    protected float mouseY;
    protected float sensivity = 10f;        
    protected bool _isActive;

    virtual protected void Start()
    {       
        Cursor.visible = false;
        inputActions = new InputSystem_Actions();        
    }

    protected virtual void Update()
    {
        if (!_isActive)
        {
            AIMod();
            return;
        }
        
    }
    
    protected virtual void RotateCamera()
    {
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        mouseX += look.x * Time.deltaTime * sensivity;
        mouseY -= look.y * Time.deltaTime * sensivity;
        mouseY = Mathf.Clamp(mouseY, -75, 75);        
    }

    public virtual void Activate()
    {        
        _isActive = true;        
        if (cmCamera != null) cmCamera.Priority = 10;        
        cmCameraGameObject = GameObject.Find("CM Camera" + _characterName);           
    }

    public virtual void Deactivate()
    {
        _isActive = false;        
        if (cmCamera != null) cmCamera.Priority = 0;        
    }

    protected virtual void AIMod()
    {

    }
}
