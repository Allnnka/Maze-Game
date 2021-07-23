using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Animation doorAnim;
    bool flag = false;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.GetChild(1).GetChild(0).childCount != 0 && !flag)
        {
            GameObject key = collider.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            if (key.name == "Key")
            {
                doorAnim.Play();
                //key.SetActive(false);
                //key.transform.parent = null;
                flag = true;
            }
        }
        
    }
}
