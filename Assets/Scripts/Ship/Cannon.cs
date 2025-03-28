using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Cannon : MonoBehaviour, IInteractable
{    
    private bool _ballLoaded = false;
    private bool _isFiring;
    [SerializeField] private float _firePower;

    [SerializeField] private GameObject _ballPrefab;    
    private Animator _animator;

    [SerializeField] Transform _ballAttackPoint;

    private VisualEffect _effect;

    [SerializeField] bool _enemyCannon = false;

    AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();  
        _effect = GetComponentInChildren<VisualEffect>();
        _effect.Stop();
        _audioSource = GetComponent<AudioSource>();
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
    public void Fire() //Потом переделать (сделать не паблик)
    {
        _ballLoaded = false;

        GameObject ball = Instantiate(_ballPrefab, _ballAttackPoint);
        SpawnEffect();
        PlayAudio();

        float currentFirePower = _firePower;

        if (_enemyCannon)
        {
            currentFirePower = Random.Range(_firePower/3, _firePower * 1.5f);
        }
        ball.GetComponent<Ball>()?.FireTheBall(transform.forward, currentFirePower, _enemyCannon);
        _isFiring = false;
        
              
    } 
    
    private void SpawnEffect()
    {
        if (_effect != null) _effect.Play();
    }

    private void PlayAudio()
    {
        _audioSource.Play();
    }
}
