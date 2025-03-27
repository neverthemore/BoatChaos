using System.Collections;
using UnityEngine;
//using static UnityEngine.Rendering.DynamicArray<T>;           Это че нахуй?

public class Ball : BaseItem
{
    private bool _friendly = true;

    private bool _isTakeDamage = false;
    protected override void Start()
    {
        base.Start();
        Name = "CannonBall";
        _whoCanEnteract = WhoCanEnteract.Pushkar;
    }
    public override void Interact(GameObject interactor)
    {        
        base.Interact(interactor);
        if (!IsInteractionAllowed)
            return;
        PickUp();

    }
    private void PickUp()
    {
        _rb.isKinematic = true; // Отключаем физику
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
    }
    public override void DropItem()
    {
        _rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }    
    public override void UseItem()
    {
        //Удаляем нахуй этот объект (!!!не проебать ошибки!!!)
        Destroy(gameObject);
    }
    public void SpawnObject(Transform placement)
    {
        Instantiate(gameObject, placement);
    }
    public void FireTheBall(Vector3 direction, float power, bool enemyBall = false)
    {
        //После выстрела если попадает в корабль, то что-то вызывает 
        //Если во вражеский, то топим
        //Если в наш, то снимаем хп

        _friendly = !enemyBall;
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(direction * power, ForceMode.Impulse);
        transform.SetParent(null);

        //Уничтожение через пару секунд (5)
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyShip>() != null && _friendly)
        {
            collision.gameObject.GetComponentInParent<EnemyShip>()?.SinkTheShip();
        }

        if (collision.gameObject.GetComponentInParent<Ship>() != null && !_friendly)
        {
            //Логика попадания снаряда по нам
            //Взрыв визуальный
            Debug.Log("Нам пизда");
            if (!_isTakeDamage)
            {
                UIBranch.Instance.SpawnBreach();
                _isTakeDamage = true;
            }
            Destroy(gameObject, 2f);
        }
    }

}
