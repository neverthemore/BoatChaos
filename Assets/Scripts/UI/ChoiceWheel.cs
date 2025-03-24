using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChoiceWheel : MonoBehaviour
{
    //�������� UI � �������, ����� � ����������� �� ��������� ����� ����� ���������   
    [SerializeField] Sprite[] backGroundImages;    

    private Image selectionWheel;
    private int _currentSelection;
    private int _selectedCharacter; 

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
        //Cursor.visible = false; 
    }

    public void Open()
    {
        wheelCanvasGroup.alpha = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //���������� ������, �������� �����, ����� � ����������� �� ��������� ������ _currentSelection
        //�� � �������� ��, �� ��� ��� UI (��������� ���������� � UI � ������ ������ �����)

    }
     
    public void Close()
    {
        if (_currentSelection >= 0 && _currentSelection <= 3)
        {
            manager.SwitchCharacter(_selectedCharacter);
            int indexOfMassive = 4 * _selectedCharacter + _currentSelection;
            _selectedCharacter = _currentSelection;
            selectionWheel.sprite = backGroundImages[indexOfMassive];
        }
        wheelCanvasGroup.alpha = 0f; 
        
        Cursor.lockState = CursorLockMode.Locked;

        //Cursor.visible = false;
        //�������� ������ � ��
    }

    private void Start()
    {
        selectionWheel = GetComponentInChildren<Image>();
        inputActions = new InputSystem_Actions();
        manager = CharacterManager.Instance;
        inputActions.Enable();
        GameObject wheel = GameObject.Find("ChoiseWheel");
        wheelCanvasGroup = wheel.GetComponent<CanvasGroup>();        
    }

    private void Update() //���� �����, ��� Close ����������� ������ ����
    {
        _selected = inputActions.Captain.CircleMenu.IsPressed();
        if (_selected) Open();
        else Close();        
    }

    private void SetSelection(int selection)
    {
        if (_selected)
        {
            _currentSelection = selection;
            int indexOfMassive = 4 * _selectedCharacter + _currentSelection;
            selectionWheel.sprite = backGroundImages[indexOfMassive];
        }
        
    }

    public void setSelectionUp() //������� ��������� ������
    {
        SetSelection(2);
    }
    public void setSelectionDown()
    {
        SetSelection(0);
    }
    public void setSelectionRight()
    {
        SetSelection(3);
    }
    public void setSelectionLeft() 
    {
        SetSelection(1);
    }



    public void SwitchCharacterToCaptain()  //���������� ����������, ��� ���� ���������� (������ ��� ������ �� UI ������)
    {
        if (_selected)
        {
            SetSelection(0);
            manager.SwitchCharacter(0);            
            _selectedCharacter = 0;            
        }
    }
    public void SwitchCharacterToFranky()
    {
        if (_selected)
        {
            SetSelection(1);
            manager.SwitchCharacter(1);
            _selectedCharacter = 1;
        }
    }
    public void SwitchCharacterToUsopp() 
    {
        if (_selected)
        {
            SetSelection(2);
            manager.SwitchCharacter(2);
            _selectedCharacter = 2;
        }        
    }
    public void SwitchCharacterToChopper() 
    {
        if (_selected)
        {
            SetSelection(3);
            manager.SwitchCharacter(3);
            _selectedCharacter = 3;
        }
    }
    public int GetCurrentSelection() //�� ����� ����� �������, ��
    {
        return _currentSelection;
    }
}
