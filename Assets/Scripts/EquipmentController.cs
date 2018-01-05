using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour {
	public Equipment eq;

	// Use this for initialization
	void Start () {
		GameObject go=(GameObject)Instantiate(eq.modele, transform.position, transform.rotation);
		go.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
