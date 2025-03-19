using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //��������
    public static CharacterManager Instance;

    [SerializeField] private BaseCharacter[] characters;
    //����� ������� ���� � ���, ����� � ��� ������ �������� (������?)
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

        //�������� �� ��, ����� �� ����� �� ������� �������?
        _currentCharacter = characters[index];
        _currentCharacter.Activate();
    }    
}
