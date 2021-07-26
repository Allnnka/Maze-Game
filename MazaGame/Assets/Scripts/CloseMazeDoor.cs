using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMazeDoor : MonoBehaviour
{
    public Animation doorAnim;
    bool flag = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !flag)
        {
            doorAnim.Play("DoorMazeClose");
            flag = true;
        }
    }
}
