using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected string _characterName;
    //[SerializeField] protected GameObject characterPrefab; //�� ����, ��� �����

    protected bool _isActive;

    protected virtual void Update()
    {
        if (!_isActive)
        {
            AIMod();
            return;
        }

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
