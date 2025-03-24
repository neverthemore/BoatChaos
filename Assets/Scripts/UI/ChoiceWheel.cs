using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChoiceWheel : MonoBehaviour
{
    //Открытие UI с колесом, затем в зависимости от положения мышки выбор персонажа   
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
        Cursor.visible = false; 
    }

    public void Open()
    {
        wheelCanvasGroup.alpha = 1f;        
        Cursor.visible = true;
    }
     
    public void Close()
    {
        if (_selectedCharacter >= 0 && _selectedCharacter <= 3)
        {            
            manager.SwitchCharacter(_selectedCharacter);
            int indexOfMassive = 4 * _selectedCharacter + _currentSelection;
            _selectedCharacter = _currentSelection;
            selectionWheel.sprite = backGroundImages[indexOfMassive];
        }
        wheelCanvasGroup.alpha = 0f;        
        Cursor.visible = false;        
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

    private void Update() //Есть трабл, что Close запускается каждый кадр
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

    public void setSelectionUp() //Выборка выделения кнопки
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



    public void SwitchCharacterToCaptain()  //Прикольная реализация, это надо переделать (скрипт для кнопок на UI именно)
    {
        if (_selected)
        {
            manager.SwitchCharacter(0);
            SetSelection(0);
            _selectedCharacter = 0;
        }
    }
    public void SwitchCharacterToFranky()
    {
        if (_selected)
        {
            manager.SwitchCharacter(1);
            SetSelection(1);
            _selectedCharacter = 1;
        }
    }
    public void SwitchCharacterToUsopp() 
    {
        if (_selected)
        {
            manager.SwitchCharacter(2);
            SetSelection(2);
            _selectedCharacter = 2;
        }        
    }
    public void SwitchCharacterToChopper() 
    {
        if (_selected)
        {
            manager.SwitchCharacter(3);
            SetSelection(3);
            _selectedCharacter = 3;
        }
    }
    public int GetCurrentSelection() //Мб через ивент сделать, хз
    {
        return _currentSelection;
    }

}
