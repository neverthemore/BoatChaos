using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{    
    [SerializeField] protected string _characterName;
    protected GameObject cmCameraGameObject;
    [SerializeField] protected CinemachineCamera cmCamera; //���� ������� ���, ����� �������� � ��������
    protected float mouseX;
    protected float mouseY;
    protected float sensivity = 10f;
    Camera camera;
    //[SerializeField] protected GameObject characterPrefab; //�� ����, ��� �����

    protected bool _isActive;

    virtual protected void Start()
    {
        camera = Camera.main;
        cmCameraGameObject = GameObject.Find("CM Camera");
        //cmCamera = GetComponentInChildren<CinemachineCamera>(); // ���� ��� �� ��� ��� ���������
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
        else Debug.Log("null");
        //+ ������ � ����������
        //������� ��������� ������
    }

    public virtual void Deactivate()
    {
        _isActive = false;
        if (cmCamera != null) cmCamera.Priority = 0;
        else Debug.Log("null");
        //+ ������
        //�������� ��������� ������
    }

    protected virtual void AIMod()
    {

    }
}
