using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HintInterract : MonoBehaviour
{
    [Header("Настройки Подсказки")]
    [SerializeField] private string _hintText;    
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Material outlineMaterial;

    private OutlineController currentOutlinedObject;    
    private GameObject _toolTip;
    private CharacterManager _manager;
    private BaseCharacter _activeCharacter;

    protected virtual void Start()
    {
        _manager = GameObject.Find("Manager").GetComponent<CharacterManager>();        
        _toolTip = Instantiate(Resources.Load<GameObject>("Hint"));
        _toolTip.SetActive(false);
    }    
    private void HandleOutline()
    {
        Transform cameraTransform = _activeCharacter.GetComponentInChildren<CinemachineCamera>()?.gameObject.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            OutlineController outlineController = hit.collider.GetComponent<OutlineController>();

            if (outlineController != null && outlineController != currentOutlinedObject)
            {
                // Убираем обводку с предыдущего объекта
                if (currentOutlinedObject != null)
                    currentOutlinedObject.DisableOutline();

                // Добавляем обводку новому объекту
                currentOutlinedObject = outlineController;
                currentOutlinedObject.EnableOutline();
            }
        }
        else
        {
            // Убираем обводку, если не смотрим на объект
            if (currentOutlinedObject != null)
            {
                currentOutlinedObject.DisableOutline();
                currentOutlinedObject = null;
            }
        }
    }    
    void Update()
    {
        HandleOutline();        
    }    
}

