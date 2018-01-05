using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLoot : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 30f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "player") {
			if(other.gameObject.GetComponent<InventoryController>().AddEquipment(gameObject.transform.GetChild (0).GetComponent<EquipmentController>().eq))
			    Destroy(gameObject);

        }
	}
}
