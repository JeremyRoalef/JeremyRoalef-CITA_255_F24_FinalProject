using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static GameObject instance;
    private void Awake()
    {
        //Singleton Pattern
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
