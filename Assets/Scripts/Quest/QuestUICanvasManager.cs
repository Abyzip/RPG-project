using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUICanvasManager : MonoBehaviour {
    public GameObject QUIManager;
    private bool isOpen = false;
	// Use this for initialization
	void Start () {
        QUIManager.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K)) {

            if (isOpen)
            {
                QUIManager.SetActive(false);
                isOpen = false;
            }
            else
            {
                QUIManager.SetActive(true);
                isOpen = true;
            }
        }
	}
}
