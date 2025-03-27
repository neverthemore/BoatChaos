using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HintInterract : MonoBehaviour
{
    [Header("Настройки Подсказки")]
    [SerializeField] private string _hintText;
    [SerializeField] private Color _color;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private float _interactDistance;

    private Outline _outline;
    private GameObject _toolTip;
    private CharacterManager _manager;
    private BaseCharacter _activeCharacter;

    protected virtual void Start()
    {
        _manager = GameObject.Find("Manager").GetComponent<CharacterManager>();
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
        _outline.OutlineColor = _color;
        _outline.OutlineWidth = _width;
        _toolTip = Instantiate(Resources.Load<GameObject>("Hint"));
        _toolTip.SetActive(false);
    }
    bool CheckPlayerRaycast(Transform camera)
    {        
        Ray ray = new Ray(camera.position, camera.forward);
        return Physics.Raycast(ray, out RaycastHit hit, _interactDistance) && hit.collider.gameObject == gameObject;
    }
    void Update()
    {
        _activeCharacter = _manager.FindActive();
        Transform camera = _activeCharacter.gameObject.GetComponentInChildren<CinemachineCamera>().gameObject.transform;
        if (_activeCharacter != null)
        {            
            float distance = Vector3.Distance(transform.position, camera.position);
            bool isLookingAt = CheckPlayerRaycast(camera);

            if (_outline != null)
            {
                _outline.OutlineWidth = (distance <= _interactDistance && isLookingAt) ? _width : 0f;
                Debug.Log(distance <= _interactDistance);
                _toolTip.SetActive(_outline.OutlineWidth > 0f);
            }
        }        

        // Позиционирование подсказки над предметом
        if (_toolTip.activeSelf)
        {
            Debug.Log("Открытие окна");
            _toolTip.transform.position = transform.position + Vector3.up * 0.5f;
            _toolTip.transform.LookAt(_activeCharacter.transform);
            _toolTip.GetComponentInChildren<TextMeshProUGUI>().text = _hintText;
        }   

    }    
}

