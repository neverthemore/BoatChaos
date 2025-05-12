using System.Data;
using Unity.Behavior;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public abstract class BaseCharacter : MonoBehaviour
{
    #region Base Variables
    [SerializeField] protected string _characterName;
    public string CharacterName { get { return _characterName; } }
    protected GameObject cmCameraGameObject;
    public bool _isActive;          //Активен ли сейчас персонаж  
    public Transform _itemTransform;//Место для присоединения вещей  
    #endregion

    #region Components
    protected BehaviorGraphAgent behavior;                      //ИИ Компонент
    protected CinemachineCamera cmCamera;                       //Камера
    protected InputSystem_Actions inputActions;                 //Инпуты
    protected InteractionDetector _interactionDetector;         //Для взятия вещей
    protected ItemState _itemState;                             //Ячейка инвентаря
    #endregion

    #region Rotate protected Variables
    [SerializeField] protected float mouseX;
    [SerializeField] protected float mouseY;
    public float Sensitivity = 10f;
    #endregion

    #region Illness Variables
    //protected bool _isIll;
    //public bool IsIll { get { return _isIll; } }
    //[SerializeField] private IllnesEvent _illnessEvent;    
    //[SerializeField]VisualEffect _illEffect;
    #endregion    

    virtual protected void Start()
    {
        Cursor.visible = false;

        behavior = GetComponent<BehaviorGraphAgent>();
        cmCamera = GetComponentInChildren<CinemachineCamera>();       
        _interactionDetector = gameObject.AddComponent<InteractionDetector>();
        _itemState = gameObject.AddComponent<ItemState>();
        inputActions = new InputSystem_Actions();

        if (_itemTransform == null)        
            _itemTransform = transform.Find("ItemPivot");
        
        behavior.enabled = true;
        //_illEffect.Stop();
    }

    protected virtual void Update()
    {
        if (Sensitivity != PauseMenu.MouseSense) Sensitivity = PauseMenu.MouseSense;

        if (!_isActive) return;

        if (inputActions.Crew.Use.triggered)
        {
            _interactionDetector.SendARay();
        }

        if (inputActions.Crew.PutDown.triggered)
        {
            _itemState.DropItem();
        }        
    }

    private void OnEnable()
    {
        //_illnessEvent.OnIllnessStart.AddListener(StartIll);
    }

    private void OnDisable()
    {
        //_illnessEvent.OnIllnessStart.RemoveListener(StartIll);
    }

    protected virtual void RotateCamera()
    {
        Vector2 look = inputActions.Captain.Look.ReadValue<Vector2>();
        mouseX += look.x * Time.deltaTime * Sensitivity;
        mouseY -= look.y * Time.deltaTime * Sensitivity;
        mouseY = Mathf.Clamp(mouseY, -75, 75);        
    }

    public virtual void Activate()
    {        
        _isActive = true;     
        behavior.enabled = false;
        if (cmCamera != null) cmCamera.Priority = 10;        
        cmCameraGameObject = GameObject.Find("CM Camera" + _characterName);           
    }

    public virtual void Deactivate()
    {
        behavior.enabled = true;
        _isActive = false;        
        if (cmCamera != null) cmCamera.Priority = 0;        
    }    
    public virtual void AddItem(BaseItem item)
    {       
        _itemState.PickUpItem(item);
    }

    public virtual void DropItem()
    {
        _itemState.DropItem();
    }

    public BaseItem GetItem()
    {
        return _itemState.Item;
    }

    //#region Illness
    //protected virtual void StartIll()  //Для события болезни
    //{
    //    int number = _illnessEvent.numberOfIllCharacter;
    //    if (CharacterManager.Instance.characters[number].name == _characterName)
    //    {
    //        Debug.Log(_characterName + " заболел");
    //        CharacterManager.Instance.characters[number]._illEffect.Play();
    //        CharacterManager.Instance.characters[number]._isIll = true;
    //    }

    //}

    //public void Cure()
    //{
    //    int number = _illnessEvent.numberOfIllCharacter;
    //    Debug.Log(_characterName + " вылечен");
    //    CharacterManager.Instance.characters[number]._illEffect.Stop();
    //    CharacterManager.Instance.characters[number]._isIll = false;
    //    _illnessEvent.Complete();
    //}
    //#endregion
}
