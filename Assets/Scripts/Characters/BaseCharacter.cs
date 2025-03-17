using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected string _characterName;
    //[SerializeField] protected GameObject characterPrefab; //�� ����, ��� �����

    protected bool _isActive;

    public virtual void Activate()
    {
        _isActive = true;
        //+ ������ � ����������
    }

    public virtual void Deactivate()
    {
        _isActive = false;
        //+ ������
    }
}
