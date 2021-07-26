using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoadManager.DontDestroyOnLoadFunction(transform.gameObject);
    }
}
public static class DontDestroyOnLoadManager { 
    static List<GameObject> _ddolObjects = new List<GameObject>();

    public static void DontDestroyOnLoadFunction(GameObject go) {
       UnityEngine.Object.DontDestroyOnLoad(go);
       _ddolObjects.Add(go);
    }

    public static void DestroyAll() {
        Debug.Log("We destroy allQQQ");
        foreach (var el in _ddolObjects)
        {
            Debug.Log(el.name);
        }
        foreach (var go in _ddolObjects)
            if(go != null)
                UnityEngine.Object.Destroy(go);
        
        _ddolObjects.Clear();
    }
    
}
