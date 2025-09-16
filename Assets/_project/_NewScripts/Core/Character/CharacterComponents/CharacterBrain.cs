using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoatGame.Core.Character.Components
{
    public class CharacterBrain
    {
        private InputAction moveAction;
        private InputAction lookAction;

        private readonly Action<Vector2> moveActionDelegat;
        private readonly Action<Vector2> lookActionDelegat;

        public CharacterBrain(Action<Vector2> moveDelegat, Action<Vector2> lookDelegat)
        {
            moveAction = InputSystem.actions.FindAction("Move");
            lookAction = InputSystem.actions.FindAction("Look");

            moveActionDelegat = moveDelegat;
            lookActionDelegat = lookDelegat;
        }

        public void Update(float deltaTime) //+ Надо бы кнопку взаимодействия обрабатывать
        {
            Vector2 moveDirection = moveAction.ReadValue<Vector2>();
            Vector2 lookDirection = lookAction.ReadValue<Vector2>();

            moveActionDelegat?.Invoke(moveDirection);
            lookActionDelegat?.Invoke(lookDirection);
        }
    }
}