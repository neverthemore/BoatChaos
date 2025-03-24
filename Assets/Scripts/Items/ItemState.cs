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
        //�������� ����� �������� Kinematic � rb
    }

    public void DropItem()
    {
        if ( _item != null )
        {
            _item.transform.SetParent(null);
            _item = null;
            Debug.Log("��������� ���������� �������");
        }
    }

    public void UseItem()
    {
        //����� ����������� -> ������� ���

    }
}
