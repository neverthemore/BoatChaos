using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HintInterract : MonoBehaviour
{
    [Header("Настройки Подсказки")]
    [SerializeField] private string _hintText;    
    [SerializeField] private float interactDistance;    
    
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
        _activeCharacter = _manager.FindActive();
        if (_activeCharacter != null)
        {
            Transform cameraTransform = _activeCharacter.GetComponentInChildren<CinemachineCamera>()?.gameObject.transform;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, LayerMask.NameToLayer("Item"))
                || Physics.Raycast(ray, out hit, interactDistance, LayerMask.NameToLayer("ItemOutline")))
            {
                Debug.Log("Смотрим на объект");
                gameObject.layer = LayerMask.NameToLayer("ItemOutline");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
        }
    }    
    void Update()
    {
        HandleOutline();        
    }    
}

