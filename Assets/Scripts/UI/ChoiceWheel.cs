using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChoiceWheel : MonoBehaviour
{
    //Открытие UI с колесом, затем в зависимости от положения мышки выбор персонажа   
    [SerializeField] Sprite[] backGroundImages;
    [SerializeField] Image[] selectionCrosses;

    private Image selectionWheel;
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
        //Cursor.visible = false; 
    }

    public void Open()
    {
        wheelCanvasGroup.alpha = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Показываем колесо, включаем мышку, далее в зависимости от положения меняем _currentSelection
        //Ну и анимация мб, но это уже UI (подсветка выбранного в UI в данный момент перса)

    }
     
    public void Close()
    {
        if (_currentSelection >= 0 && _currentSelection <= 3)
        {            
            manager.SwitchCharacter(_currentSelection);
            selectionWheel.sprite = backGroundImages[_currentSelection];
        }
        wheelCanvasGroup.alpha = 0f; 
        
        Cursor.lockState = CursorLockMode.Locked;

        //Cursor.visible = false;
        //Закрытие колеса и тд
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
            for (int i = 0; i < selectionCrosses.Length; i++)
            {
                if (i == selection)
                {
                    Color currentColor = selectionCrosses[selection].color;
                    currentColor.a = 255f;
                    selectionCrosses[selection].color = currentColor;
                }
                else
                {
                    Color currentColor = selectionCrosses[i].color;
                    currentColor.a = 0f;
                    selectionCrosses[i].color = currentColor;
                }
            }  

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
            selectionWheel.sprite = backGroundImages[0];
        }
    }
    public void SwitchCharacterToFranky()
    {
        if (_selected)
        {
            manager.SwitchCharacter(1);
            selectionWheel.sprite = backGroundImages[1];
        }
    }
    public void SwitchCharacterToUsopp() 
    {
        if (_selected)
        {
            manager.SwitchCharacter(2);
            selectionWheel.sprite = backGroundImages[2];
        }        
    }
    public void SwitchCharacterToChopper() 
    {
        if (_selected)
        {
            manager.SwitchCharacter(3);
            selectionWheel.sprite = backGroundImages[3];
        }
    }
    public int GetCurrentSelection() //Мб через ивент сделать, хз
    {
        return _currentSelection;
    }
}
