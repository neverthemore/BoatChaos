using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{    
    [SerializeField] protected string _characterName;
    protected GameObject cmCamera;
    protected float mouseX;
    protected float mouseY;
    protected float sensivity = 10f;
    Camera camera;
    //[SerializeField] protected GameObject characterPrefab; //�� ����, ��� �����

    protected bool _isActive;

    virtual protected void Start()
    {
        camera = Camera.main;
        cmCamera = GameObject.Find("CM Camera");
    }

    protected virtual void Update()
    {
        if (!_isActive)
        {
            AIMod();
            return;
        }
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Head"));
    }
    
    protected virtual void RotateCamera()
    {
        
    }

    public virtual void Activate()
    {        
        _isActive = true;
        //+ ������ � ����������
        //������� ��������� ������
    }

    public virtual void Deactivate()
    {
        _isActive = false;
        //+ ������
        //�������� ��������� ������
    }

    protected virtual void AIMod()
    {

    }
}
