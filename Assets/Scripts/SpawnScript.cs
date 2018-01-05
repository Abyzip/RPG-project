using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
	public GameObject prefabEnnemy;
    public float timeRespawn;
	// Use this for initialization
	void Start () {
		GameObject go=Instantiate (prefabEnnemy, transform.position,Quaternion.identity);
		go.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void respawn() {
        if (prefabEnnemy.tag != "ennemyBossTribu")
                StartCoroutine ("respawnCoroutine");
	}

	IEnumerator respawnCoroutine() {
		yield return new WaitForSeconds (timeRespawn);
        
		GameObject go = Instantiate (prefabEnnemy, transform.position, Quaternion.identity);
		go.transform.parent = transform;
	}
}
