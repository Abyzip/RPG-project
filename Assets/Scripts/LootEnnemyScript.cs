using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEnnemyScript : MonoBehaviour {
	public List<Equipment> listLoots;
	public List<string> listLootsName;
	public GameObject lootGo;
	// Use this for initialization
	void Start () {
		listLoots=new List<Equipment>();
		for (int i = 0; i < listLootsName.Count; i++) {
			listLoots.Add (GetComponent<EquipmentConstructor> ().getEquipment(listLootsName[i]));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void isLooting() {
		int firstRandom=Random.Range (0, 1);
		if (firstRandom == 0) {
			int randomLoot=Random.Range (0, listLoots.Count);
			lootGo.transform.GetChild(0).gameObject.GetComponent<EquipmentController>().eq=listLoots [randomLoot];
			Instantiate (lootGo, transform.position+new Vector3(0,1,0),Quaternion.identity);
		}
	}
}
