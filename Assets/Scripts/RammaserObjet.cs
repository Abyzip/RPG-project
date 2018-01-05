using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammaserObjet : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "player" && Input.GetKey(KeyCode.LeftControl))
        {
            Destroy(gameObject);
        }
    }
}
