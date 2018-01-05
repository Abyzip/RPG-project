using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsPlayer : MonoBehaviour {
	public Equipment LArm;
	public Equipment RArm;
	public GameObject slotLArm;
	public GameObject slotRArm;
	public Transform testDebug;
	public GameObject olderGoRight;
	public GameObject olderGoLeft;
	public GameObject inventoryGO;
	public int healthEquipment;
	public int criticalStrike;
	public int dgtLeft;
	public int dgtRight;
	// Use this for initialization
	void Start () {
        LArm = GetComponent<EquipmentConstructor> ().getEquipment ("Masse de paysan");
        RArm = GetComponent<EquipmentConstructor> ().getEquipment ("Hache de paysan");
        //LArm = GetComponent<EquipmentConstructor>().getEquipment("Epée de rubis");
        //RArm = GetComponent<EquipmentConstructor> ().getEquipment ("Epée éternelle");
        LArm.modele=Instantiate(LArm.modele);
		updateEquipment ("left");
		RArm.modele=Instantiate(RArm.modele);
		updateEquipment ("right");


	}

	public void updateEquipment (string leftOrRight){
        healthEquipment = LArm.health + RArm.health;
        criticalStrike = LArm.criticalStrike + RArm.criticalStrike;
        dgtLeft = LArm.degats;
        dgtRight = RArm.degats;
        afficherStats();
        if (leftOrRight == "right") {
			//RArm.modele=Instantiate(RArm.modele);
			RArm.modele.transform.parent = slotRArm.transform;
			RArm.modele.transform.localPosition = new Vector3 (0, 0, 0);
			RArm.modele.transform.localRotation = Quaternion.Euler (-90, 0, 0);
            GetComponent<HealthUIScript>().maxHealth = 500 + healthEquipment;
            int percentHealth = GetComponent<HealthUIScript>().currentHealthPercent;
            GetComponent<HealthUIScript>().currentHealth = ((GetComponent<HealthUIScript>().maxHealth / 100) * percentHealth);
        } else {
			//LArm.modele=Instantiate(LArm.modele);
			LArm.modele.transform.parent = slotLArm.transform;
			LArm.modele.transform.localPosition = new Vector3 (0, 0, 0);
			LArm.modele.transform.localRotation = Quaternion.Euler (90, -90, 90);
            GetComponent<HealthUIScript>().maxHealth = 500 + healthEquipment;
            int percentHealth = GetComponent<HealthUIScript>().currentHealthPercent;
            GetComponent<HealthUIScript>().currentHealth = ((GetComponent<HealthUIScript>().maxHealth / 100) * percentHealth);
        }
		
    }

	public void Equiper (Equipment eq, string leftOrRight){
		if (leftOrRight == "right") {
			RArm = eq;
			Desequiper ("right");
			updateEquipment ("right");
		} else {
			LArm = eq;
			Desequiper ("left");
			updateEquipment ("left");
		}
	}

	public void Desequiper (string leftOrRight){
		if (leftOrRight == "right") {
			GameObject go = slotRArm.transform.GetChild (0).gameObject;
			go.transform.parent = inventoryGO.transform;
			go.transform.localPosition = new Vector3 (0, 0, 0);
			go.transform.localRotation = Quaternion.Euler (-90, 0, 0);

		} else {
			GameObject go = slotLArm.transform.GetChild (0).gameObject;
			go.transform.parent = inventoryGO.transform;
			go.transform.localPosition = new Vector3 (0, 0, 0);
			go.transform.localRotation = Quaternion.Euler (-90, 0, 0);
		}
			//Destroy(slotRArm.transform.GetChild (0).gameObject);

			//Destroy(slotLArm.transform.GetChild (0).gameObject);
	}

	public void afficherStats() {
		Debug.Log ("HEALTH : " + healthEquipment);
		Debug.Log ("CRITICAL : " + criticalStrike);
		Debug.Log ("DGT LEFT : " + dgtLeft);
		Debug.Log ("DGT RIGHT : " + dgtRight);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
