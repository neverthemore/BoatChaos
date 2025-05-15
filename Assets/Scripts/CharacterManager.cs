using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField] public BaseCharacter[] characters;

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

        //ѕроверка на то, чтобы не выйти за пределы массива?
        _currentCharacter = characters[index];
        _currentCharacter.Activate();
    }    
}
