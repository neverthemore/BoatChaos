using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{    
    private bool _ballLoaded = false;
    private bool _isFiring;
    [SerializeField] private float _firePower;

    [SerializeField] private GameObject _ballPrefab;    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();        
    }

    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;
        if (!_ballLoaded) //Не заряжена -> заряжаем
        {
            if (interactor.GetComponent<BaseCharacter>().GetItem() != null)
                if (interactor.GetComponent<BaseCharacter>().GetItem().Name == "CannonBall")
                {
                //Удаляем это ядро, пушка заряжена
                interactor.GetComponent<BaseCharacter>().GetItem().UseItem();
                interactor.GetComponent<Animator>()?.SetTrigger("use");
                _ballLoaded = true;
                Debug.Log("Ядро загружено");
                }
        }
        else
        {
            _animator.SetTrigger("firing");
        }
    }
    private void Fire()
    {
        Debug.Log("FIRE");
        _ballLoaded = false;
        GameObject ball = Instantiate(_ballPrefab, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
        ball.GetComponent<Ball>()?.FireTheBall(transform.forward, _firePower);
        _isFiring = false;        
              
    }    
}
