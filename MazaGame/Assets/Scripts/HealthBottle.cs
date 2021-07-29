using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBottle : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "HealthBottle";
        }
    }
    public override void OnDrop()
    {
        //Destroy(gameObject);
        gameObject.SetActive(true);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.eulerAngles = Vector3.zero;
    }
}
