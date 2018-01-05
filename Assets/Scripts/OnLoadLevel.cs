using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadLevel : MonoBehaviour {

    void OnLevelWasLoad(int level)
    {
        if(level == 1)
        {
            Destroy(gameObject);
        }
        Debug.Log("Destroyed");
    }
}
