using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HintInterract : MonoBehaviour
{
    public static HintInterract Instance;

    [SerializeField] private float interactDistance;
    private Vector3 _hintPosition;

    private GameObject _hintWindow;
    private GameObject _currentObj;    
    private CharacterManager _manager;
    private BaseCharacter _activeCharacter;

    protected virtual void Start()
    {
        _manager = GameObject.Find("Manager").GetComponent<CharacterManager>();  
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

            int layerMask = (1 << LayerMask.NameToLayer("Item")) | (1 << LayerMask.NameToLayer("ItemOutline"));
            //if (Physics.Raycast(ray, out hit, interactDistance, 1 << LayerMask.NameToLayer("Item")) ||
            //    Physics.Raycast(ray, out hit, interactDistance, 1 << LayerMask.NameToLayer("ItemOutline")))
            if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
            {
                if (hit.collider != null && hit.collider.gameObject != _currentObj)
                {
                    _currentObj = hit.collider.gameObject;
                    /*
                    if (_currentObj.transform.parent.GetComponentInChildren<Canvas>() != null)
                    {
                        _hintWindow = _currentObj.transform.parent.GetComponentInChildren<Canvas>()?.gameObject;
                        _hintWindow.transform.position = _currentObj.transform.position + new Vector3(0, 1f, 0);
                        _hintWindow.transform.LookAt(_activeCharacter.transform.position);
                    }
                    */
                    _currentObj.layer = LayerMask.NameToLayer("ItemOutline");                                        
                    
                    ShowHint();
                }                
            }
            else if(_currentObj != null)
            {
                _currentObj.layer = LayerMask.NameToLayer("Item");
                HideHint();
                _currentObj = null;

                
            }

            UpdateURPRenderer();
        }
    }   
    void ShowHint()
    {
        /*
        if (_hintWindow == null) return;
        Canvas canvas = _hintWindow.GetComponent<Canvas>();
        canvas.enabled = true;  
        */
        if (_currentObj.GetComponent<IPromtable>() != null)
        {
            if (_currentObj.GetComponent<IPromtable>().NeedToShowPromt())
            {
                _currentObj.GetComponent<IPromtable>().ShowPromt();
            }
        }
    }   

    void HideHint()
    {
        /*
        if (_hintWindow == null) return;
        Canvas canvas = _hintWindow.GetComponent<Canvas>();
        canvas.enabled = false;
        */
        if (_currentObj.GetComponent<IPromtable>() != null)
        {
            _currentObj.GetComponent<IPromtable>().HidePromt();            
        }
    }

    void Update()
    {
        HandleOutline();        
    }    
}

