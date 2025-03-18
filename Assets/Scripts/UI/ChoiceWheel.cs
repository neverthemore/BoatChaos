using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceWheel : MonoBehaviour
{
    //Открытие UI с колесом, затем в зависимости от положения мышки выбор персонажа
    private int _currentSelection;

    public void Open() 
    {
        //Показываем колесо, включаем мышку, далее в зависимости от положения меняем _currentSelection
        //Ну и анимация мб, но это уже UI (подсветка выбранного в UI в данный момент перса)
        
    }

    public void Close()
    {
        //Закрытие колеса и тд
    }

    public int GetCurrentSelection() //Мб через ивент сделать, хз
    {
        return 0;
    }
}
