
using UnityEngine;

[RequireComponent(typeof(BaseCharacter))]
public class InteractionDetector : MonoBehaviour
{
    //Используется в BaseCharacter
    [SerializeField]private float _interactionRange = 2.0f;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void SendARay()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactionRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}
