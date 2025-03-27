using System.Collections;
using UnityEngine;

public class Breach : MonoBehaviour, IFixable
{
    //Весит на объекте
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
        yield return new WaitUntil(() => !UIBranch.Instance.IsActive);  //Ждем пока UI не закроется
        //Если выполнено, то пробоина выключается и перестает хп тратить
        //Если не выполнено, то ничего не делаем
        _isCoroutineStarted = false;
    }

    public void StartFix()      //Скрипт для начала фикса (когда навелись и нажали E)
    {
        OpenUI();
        //Пока UI открыт мы ждем корутиной когда она закроется, после чего получаем bool (выполнено или нет)
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
        //Включает визуал
        _isActive = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("Протечка");
    }

    public void DeactivateBreach()
    {
        _isActive = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Починили");
    }
}
