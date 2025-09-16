using BoatGame.Core.Character.Components;
using Unity.Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoatGame.Core.Character
{
    public class Character : MonoBehaviour
    {
        private CharacterController controller;
        private CinemachineCamera cmCamera;

        private IMover m_PlayerMover;
        private IRotator m_Rotator;


        private CharacterBrain _brain;
        private Components.InteractionDetector m_InteractDetector;

        [Header("Настройки движения")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float sensetivity = 90f;
        [Header("Другие настройки")]
        [SerializeField] private float interactionRadius = 2f;

        private bool IsActive = false;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            cmCamera = GetComponentInChildren<CinemachineCamera>();
        }

        private void Start()
        {                        

            m_PlayerMover = new CharacterPlayerMover(transform, controller, moveSpeed);
            m_Rotator = new CharacterPlayerRotator(transform, cmCamera, sensetivity);

            _brain = new CharacterBrain(m_PlayerMover.SetDirection, m_Rotator.SetDirection);

            m_InteractDetector = new Components.InteractionDetector(gameObject, cmCamera.gameObject.transform, interactionRadius);
        }

        private void Update()
        {
            if (IsActive)
            {
                m_PlayerMover.Update(Time.deltaTime);
                m_Rotator.Update(Time.deltaTime);
            }
            else
            {

            }
            _brain.Update(Time.deltaTime);
        }

        public void ActivateCharacter()
        {
            //Переключаем управление на игрока
            IsActive = true;
            cmCamera.Priority = 10;
        }

        public void DeactivateCharacter()
        {
            //Переключаем на ИИ
            IsActive = false;
            cmCamera.Priority = -1;
        }
    }
}