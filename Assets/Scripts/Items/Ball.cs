using System.Collections;
using UnityEngine;

public class Ball : BaseItem{
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
    protected override void PickUp()
    {
        base.PickUp();
    }
    public override void DropItem()
    {
        base.DropItem();
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
            Debug.Log("Попали во врага: " + _friendly);
        }

        if (collision.gameObject.GetComponentInParent<Ship>() != null && !_friendly)
        {
            Debug.Log("Попали в нас");
            //Взрыв визуальный
            if (!_isTakeDamage)
            {
                UIBranch.Instance.SpawnBreach();
                _isTakeDamage = true;
            }
            Destroy(gameObject, 2f);
        }
    }

}
