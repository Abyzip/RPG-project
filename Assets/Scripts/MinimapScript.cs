using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {
    public Transform transformPlayer;

   
    // Update is called once per frame
    void Update () {
        transform.position = transformPlayer.position + new Vector3(0, 50, 0);

    }
}
