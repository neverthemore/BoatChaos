using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //Синглтон
    public static CharacterManager Instance;

    [SerializeField] private BaseCharacter[] characters;
    //Нужно хранить инфу о том, какой у нас сейчас персонаж (Индекс?)
    private BaseCharacter _currentCharacter;

    private void Awake()
    {
        Instance = this;
        InitializeCharacters();
    }

    private void InitializeCharacters()
    {
        foreach (var character in characters)
        {
            character.Deactivate();
        }
        SwitchCharacter(0);
    }

    public void SwitchCharacter(int index)
    {
        if (_currentCharacter != null)
        {
            _currentCharacter.Deactivate();
        }

        //Проверка на то, чтобы не выйти за пределы массива?
        _currentCharacter = characters[index];
        _currentCharacter.Activate();
    }    
}
