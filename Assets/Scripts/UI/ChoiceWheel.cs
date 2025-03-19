using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //Открытие UI с колесом, затем в зависимости от положения мышки выбор персонажа
    private int _currentSelection;
    InputSystem_Actions inputActions;
    GameObject wheel;
    CharacterManager manager;
    bool _selected = false;

    public void Open() 
    {
        wheel.SetActive(true);
        //Показываем колесо, включаем мышку, далее в зависимости от положения меняем _currentSelection
        //Ну и анимация мб, но это уже UI (подсветка выбранного в UI в данный момент перса)
        
    }

    public void Close()
    {
        wheel.SetActive(false);
        //Закрытие колеса и тд
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
    public int GetCurrentSelection() //Мб через ивент сделать, хз
    {
        return 0;
    }
}
