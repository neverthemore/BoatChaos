using System.Collections;
using UnityEngine;

public class Breach : MonoBehaviour, IFixable
{
    //����� �� �������
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _inSeconds = 2f;

    private float _currentCooldown = 0;

    bool _isActive = false;
    public bool IsActive => _isActive;

    bool _isUIOpen = false;
    bool _isCoroutineStarted = false;

    private void Update()
    {
        if (_isUIOpen && !_isCoroutineStarted)
        {
            _isCoroutineStarted = true;
            StartCoroutine(WaitUI());
        }

        if (_isActive && _currentCooldown <= 0)
        {
            //����
        }
    }

    IEnumerator WaitUI()
    {
        yield return new WaitUntil(() => !UIBranch.Instance.IsActive);  //���� ���� UI �� ���������
        //���� ���������, �� �������� ����������� � ��������� �� �������
        //���� �� ���������, �� ������ �� ������
        _isCoroutineStarted = false;
    }

    public void StartFix()      //������ ��� ������ ����� (����� �������� � ������ E)
    {
        OpenUI();
        //���� UI ������ �� ���� ��������� ����� ��� ���������, ����� ���� �������� bool (��������� ��� ���)
    }

    public void OpenUI()
    {
        UIBranch.Instance.OpenUI();
    }


    public bool NeedToFix()
    {
        return _isActive;
    }

    public void ActivateBreach()
    {
        //�������� ������
        _isActive = true;
        _currentCooldown = 0;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("��������");
    }

    public void DeactivateBreach()
    {
        _isActive = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("��������");
    }
}
