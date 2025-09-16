using BoatGame.Core.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoatGame.Core.Tests
{
    public class BootsTrap : MonoBehaviour
    {
        [SerializeField] private Character.Character captain;
        [SerializeField] private Character.Character techman;

        private Team team = new Team();

        private void Start()
        {
            team.UnlockMember(captain);
            team.UnlockMember(techman);

            team.SwitchCharacter(0);
        }

        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Нажата ЛКМ");
                team.SwitchCharacter(0);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Нажата ПКМ");
                team.SwitchCharacter(1);
            }
        }
    }
}