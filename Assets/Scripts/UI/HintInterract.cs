using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HintInterract : MonoBehaviour
{
    [Header("Настройки Подсказки")]
    [SerializeField] private string _hintText;    
    [SerializeField] private float interactDistance;

    private GameObject _currentObj;
    private GameObject _toolTip;
    private CharacterManager _manager;
    private BaseCharacter _activeCharacter;

    protected virtual void Start()
    {
        _manager = GameObject.Find("Manager").GetComponent<CharacterManager>();        
        _toolTip = Instantiate(Resources.Load<GameObject>("Hint"));
        _toolTip.SetActive(false);
    }
    private void UpdateURPRenderer()
    {
        var renderer = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        renderer.GetType().GetMethod("OnValidate")?.Invoke(renderer, null);
    }

    private void HandleOutline()
    {
        _activeCharacter = _manager.FindActive();
        if (_activeCharacter != null)
        {
            Transform cameraTransform = _activeCharacter.GetComponentInChildren<CinemachineCamera>()?.gameObject.transform;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, 1 << LayerMask.NameToLayer("Item")) ||
                Physics.Raycast(ray, out hit, interactDistance, 1 << LayerMask.NameToLayer("ItemOutline")))
            {
                if (hit.collider != null && hit.collider.gameObject != _currentObj)
                {
                    Debug.Log("Смотрим на объект");
                    _currentObj = hit.collider.gameObject;
                    _currentObj.layer = LayerMask.NameToLayer("ItemOutline");                    
                    _currentObj.GetComponentInChildren<MeshRenderer>().gameObject.layer = LayerMask.NameToLayer("ItemOutline");
                }                
            }
            else if(_currentObj != null)
            {
                _currentObj.layer = LayerMask.NameToLayer("Item");
                _currentObj.GetComponentInChildren<MeshRenderer>().gameObject.layer = LayerMask.NameToLayer("Item");
                _currentObj = null;
            }
            UpdateURPRenderer();
        }
    }    
    void Update()
    {
        HandleOutline();        
    }    
}

