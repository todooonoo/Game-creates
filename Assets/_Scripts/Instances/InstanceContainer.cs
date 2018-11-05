using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A container for all static instance objects
public class InstanceContainer : MonoBehaviour {

    private static InstanceContainer instance;
    
	private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        var singletons = FindObjectsOfType<BaseSingleton>();
        for (int i = 0; i < singletons.Length; i++)
            singletons[i].AssignInstance();
    }
}
