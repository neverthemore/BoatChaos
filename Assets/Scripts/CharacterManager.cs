using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //Синглтон
    public static CharacterManager Instance;

    [SerializeField] public BaseCharacter[] characters;
    //Нужно хранить инфу о том, какой у нас сейчас персонаж (Индекс?)
    private BaseCharacter _currentCharacter;

    public BaseCharacter FindActive()
    {
        foreach (BaseCharacter character in characters) {
            if (character._isActive) return character;
        }        
        return null;
    }

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
