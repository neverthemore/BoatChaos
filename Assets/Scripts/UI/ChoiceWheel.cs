using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //�������� UI � �������, ����� � ����������� �� ��������� ����� ����� ���������
    private int _currentSelection;

    public void Open() 
    {
        //���������� ������, �������� �����, ����� � ����������� �� ��������� ������ _currentSelection
        //�� � �������� ��, �� ��� ��� UI (��������� ���������� � UI � ������ ������ �����)
        
    }

    public void Close()
    {
        //�������� ������ � ��
    }

    public int GetCurrentSelection() //�� ����� ����� �������, ��
    {
        return 0;
    }
}
