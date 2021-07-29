using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
            var g= GameObject.Find("HUD");
            if (g != null)
            {
                g.gameObject.SetActive(false);
            }
            
        }
    }
}
