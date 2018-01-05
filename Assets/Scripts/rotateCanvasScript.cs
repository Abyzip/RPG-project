using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCanvasScript : MonoBehaviour {
	private GameObject playerCamera;

	void Start() {
		playerCamera = GameObject.FindWithTag("MainCamera");
	}
	// Update is called once per frame
	void Update () {
		transform.LookAt(playerCamera.transform);
	}
}
