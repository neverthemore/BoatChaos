using UnityEngine;

namespace BoatGame.Core.Character.Components
{
    public class InteractionDetector
    {
        private GameObject interactor;
        private readonly Transform cameraTransform;
        private float _interactionRange;

        public InteractionDetector(GameObject interactor, Transform cameraTransform, float interactionRange = 2f)
        {
            this.interactor = interactor;
            this.cameraTransform = cameraTransform;
            _interactionRange = interactionRange;
        }

        public void CheckInteraction()
        {
            Ray ray = new Ray(cameraTransform.transform.position, cameraTransform.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _interactionRange))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(interactor);
                }
            }
        }
    }
}