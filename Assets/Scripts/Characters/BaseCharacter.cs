using System.Data;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private IllnesEvent _illnessEvent;


    [SerializeField] protected string _characterName;
    public string CharacterName { get { return _characterName; } }

    protected GameObject cmCameraGameObject;

    [SerializeField] protected CinemachineCamera cmCamera; 
    protected InputSystem_Actions inputActions;

    #region Rotate protected Variables
    [SerializeField] protected float mouseX;
    [SerializeField] protected float mouseY;
    public float Sensitivity = 10f;
    #endregion

    protected ItemState _itemState;            //Ячейка инвентаря
    public Transform _itemTransform;     //Место для присоединения вещей

    [SerializeField]VisualEffect _illEffect;


    InteractionDetector _interactionDetector;

    public bool _isActive; //Активен ли сейчас персонаж

    protected bool _isIll;
    public bool IsIll { get { return _isIll; } }


    virtual protected void Start()
    {       
        Cursor.visible = false;
        inputActions = new InputSystem_Actions();   
        _interactionDetector = gameObject.AddComponent<InteractionDetector>();
        _itemState = gameObject.AddComponent<ItemState>();
        if (_itemTransform == null)
        {
            _itemTransform = transform.Find("ItemPivot");
        }

        _illEffect.Stop();
    }

    protected virtual void Update()
    {
        if (Sensitivity != PauseMenu.MouseSense) Sensitivity = PauseMenu.MouseSense;

        if (_isIll || !_isActive) return;

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
        _illnessEvent.OnIllnessStart.AddListener(StartIll);

    }

    private void OnDisable()
    {
        _illnessEvent.OnIllnessStart.RemoveListener(StartIll);
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
        if (cmCamera != null) cmCamera.Priority = 10;        
        cmCameraGameObject = GameObject.Find("CM Camera" + _characterName);           
    }

    public virtual void Deactivate()
    {
        _isActive = false;        
        if (cmCamera != null) cmCamera.Priority = 0;        
    }

    protected virtual void AIMod()
    {

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

    #region Illness
    protected virtual void StartIll()  //Для события болезни
    {
        Debug.Log(_characterName + " заболел");
        _illEffect.Play();
        _isIll = true;
    }

    public void Cure()
    {
        Debug.Log(_characterName + " вылечен");
        _illEffect.Stop();
        _isIll = false;
        _illnessEvent.HealOneCharacter();
    }
    #endregion
}
