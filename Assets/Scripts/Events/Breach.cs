using System.Collections;
using UnityEngine;

public class Breach : MonoBehaviour, IFixable
{
    //����� �� �������
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
