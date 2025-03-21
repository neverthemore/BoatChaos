using System.Data;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseCharacter : MonoBehaviour
{    
    [SerializeField] protected string _characterName;
    protected GameObject cmCameraGameObject;
    [SerializeField] protected CinemachineCamera cmCamera; //���� ������� ���, ����� �������� � ��������
    protected float mouseX;
    InputSystem_Actions inputActions;
    protected float mouseY;
    protected float sensivity = 10f;
    Camera camera;
    //[SerializeField] protected GameObject characterPrefab; //�� ����, ��� �����

    protected bool _isActive;

    virtual protected void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
        //inputActions = new InputSystem_Actions();        
    }

    protected virtual void Update()
    {
        if (!_isActive)
        {
            AIMod();
            return;
        }
        //camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Head")); !!!��� ���� ������� ����� ������
                                                                       //�� ������������ �� ������!!!
    }
    
    protected virtual void RotateCamera()
    {
        
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
