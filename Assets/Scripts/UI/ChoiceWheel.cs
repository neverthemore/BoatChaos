using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //�������� UI � �������, ����� � ����������� �� ��������� ����� ����� ���������
    private int _currentSelection;
    InputSystem_Actions inputActions;    
    CanvasGroup wheelCanvasGroup;
    CharacterManager manager;
    bool _selected = false;

    private void OnDestroy()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
        }
        Cursor.visible = false; 
    }

    public void Open()
    {
        wheelCanvasGroup.alpha = 1f;
        Cursor.visible = true;
        //���������� ������, �������� �����, ����� � ����������� �� ��������� ������ _currentSelection
        //�� � �������� ��, �� ��� ��� UI (��������� ���������� � UI � ������ ������ �����)

    }

    public void Close()
    {
        if (_currentSelection >= 0 && _currentSelection <= 3)
        {            
            manager.SwitchCharacter(_currentSelection);
        }
        wheelCanvasGroup.alpha = 0f;
        Cursor.visible = false;
        //�������� ������ � ��
    }

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        manager = CharacterManager.Instance;
        inputActions.Enable();
        GameObject wheel = GameObject.Find("Wheel");
        wheelCanvasGroup = wheel.GetComponent<CanvasGroup>();        
    }

    private void Update()
    {
        _selected = inputActions.Captain.CircleMenu.IsPressed();
        if (_selected) Open();
        else Close();
    }

    public void setSelectionUp() { _currentSelection = 2; }
    public void setSelectionDown() { _currentSelection = 0; }
    public void setSelectionRight() { _currentSelection = 3; }
    public void setSelectionLeft() { _currentSelection = 1; }

    public void SwitchCharacterToCaptain() { manager.SwitchCharacter(0); }
    public void SwitchCharacterToFranky() { manager.SwitchCharacter(1); }
    public void SwitchCharacterToUsopp() { manager.SwitchCharacter(2); }
    public void SwitchCharacterToChopper() { manager.SwitchCharacter(3); }
    public int GetCurrentSelection() //�� ����� ����� �������, ��
    {
        return _currentSelection;
    }
}
