using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //Открытие UI с колесом, затем в зависимости от положения мышки выбор персонажа
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
        //Показываем колесо, включаем мышку, далее в зависимости от положения меняем _currentSelection
        //Ну и анимация мб, но это уже UI (подсветка выбранного в UI в данный момент перса)

    }

    public void Close()
    {
        if (_currentSelection >= 0 && _currentSelection <= 3)
        {            
            manager.SwitchCharacter(_currentSelection);
        }
        wheelCanvasGroup.alpha = 0f;        
        Cursor.visible = false;
        //Закрытие колеса и тд
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

    public void setSelectionUp() { if (_selected) _currentSelection = 2; }
    public void setSelectionDown() { if (_selected) _currentSelection = 0; }
    public void setSelectionRight() { if (_selected) _currentSelection = 3; }
    public void setSelectionLeft() { if (_selected) _currentSelection = 1; }

    public void SwitchCharacterToCaptain() { if (_selected) manager.SwitchCharacter(0); }
    public void SwitchCharacterToFranky() { if (_selected) manager.SwitchCharacter(1); }
    public void SwitchCharacterToUsopp() { if (_selected) manager.SwitchCharacter(2); }
    public void SwitchCharacterToChopper() { if (_selected) manager.SwitchCharacter(3); }
    public int GetCurrentSelection() //Мб через ивент сделать, хз
    {
        return _currentSelection;
    }
}
