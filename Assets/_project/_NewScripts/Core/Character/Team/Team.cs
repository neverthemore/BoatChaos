using UnityEngine;
using System.Collections.Generic;

namespace BoatGame.Core.Character
{
    public class Team
    {
        //Содержит список персонажей (Character)
        private List<Character> crew = new List<Character>();

        private Character currentCharacter;

        public void UnlockMember(Character character)
        { 
            crew.Add(character);
        }

        public void SwitchCharacter(int id)
        {
            if (currentCharacter != null)
                currentCharacter.DeactivateCharacter();

            currentCharacter = crew[id];
            currentCharacter.ActivateCharacter();
        }
        
    }
}