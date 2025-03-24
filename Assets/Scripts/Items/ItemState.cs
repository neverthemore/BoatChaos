using UnityEngine;

public class ItemState : MonoBehaviour
{
    private BaseItem _item;
    public BaseItem Item {  get { return _item; } }

    public void PickUpItem(BaseItem newItem)
    {
        if (newItem == _item) return;
        if (_item != null) DropItem();

        _item = newItem;
        newItem.transform.SetParent(gameObject.GetComponent<BaseCharacter>()._itemTransform);
        newItem.transform.localPosition = Vector3.zero;
        //Возможно нужно включить Kinematic у rb
    }

    public void DropItem()
    {
        if ( _item != null )
        {
            _item.transform.SetParent(null);
            _item = null;
            Debug.Log("Выбросили предыдуший предмет");
        }
    }

    public void UseItem()
    {
        //Айтем активирован -> удаляем его

    }
}
