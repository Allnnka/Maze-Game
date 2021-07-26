using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorInMaze : MonoBehaviour
{
    public Animation doorAnim;
    bool flagDoor = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WE hereeee");
        if (other.gameObject.name == "Player" && !flagDoor)
        {
            doorAnim.Play("DoorAnimMaze");
            flagDoor = true;
        }
    }
    
    
}
