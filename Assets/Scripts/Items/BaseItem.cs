using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class BaseItem : MonoBehaviour
{
    //������ ��� ������
    public string Name; //�� ����� ����� ������ Scriptable
    protected Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public virtual void UseItem()
    {
        //������ �������������, �������� ����������� �������
    }
}
