using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
   public Inventory _Inventory;
   public void OnItemCklicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ImageItem").GetComponent<ItemDragHandler>();
        IInventoryItem item = dragHandler.Item;

        _Inventory.UseItem(item);
        item.OnUse();
    }
}
