using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //�������� UI � �������, ����� � ����������� �� ��������� ����� ����� ���������
    private int _currentSelection;
    InputSystem_Actions inputActions;
    GameObject wheel;
    CharacterManager manager;
    bool _selected = false;

    public void Open() 
    {
        wheel.SetActive(true);
        //���������� ������, �������� �����, ����� � ����������� �� ��������� ������ _currentSelection
        //�� � �������� ��, �� ��� ��� UI (��������� ���������� � UI � ������ ������ �����)
        
    }

    public void Close()
    {
        wheel.SetActive(false);
        //�������� ������ � ��
    }

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        manager = CharacterManager.Instance;
        inputActions.Enable();
        wheel = GameObject.Find("Wheel");
        wheel.SetActive(false);
    }

    private void Update()
    {
        _selected = inputActions.Captain.CircleMenu.IsPressed();
        if (_selected) Open();
        else Close();
    }
    public void SwitchCharacterToCaptain() { manager.SwitchCharacter(0); }
    public void SwitchCharacterToFranky() { manager.SwitchCharacter(1); }
    public void SwitchCharacterToUsopp() { manager.SwitchCharacter(2); }
    public void SwitchCharacterToChopper() { manager.SwitchCharacter(3); }
    public int GetCurrentSelection() //�� ����� ����� �������, ��
    {
        return 0;
    }
}
