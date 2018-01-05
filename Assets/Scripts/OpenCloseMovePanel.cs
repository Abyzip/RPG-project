using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseMovePanel : MonoBehaviour {

    public GameObject FrontText;
    public GameObject BackText;
    public GameObject LeftText;
    public GameObject RightText;
    public GameObject Click;
    public GameObject DashText;
    public GameObject SoinText;
    public GameObject EnrageText;
    public GameObject DialogText;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine("ShowMoves");
        }
    }

    IEnumerator ShowMoves()
    {
        FrontText.SetActive(true);
        BackText.SetActive(true);
        LeftText.SetActive(true);
        RightText.SetActive(true);
        Click.SetActive(true);
        DashText.SetActive(true);
        SoinText.SetActive(true);
        EnrageText.SetActive(true);
        DialogText.SetActive(true);
        yield return new WaitForSeconds(8);
        FrontText.SetActive(false);
        BackText.SetActive(false);
        LeftText.SetActive(false);
        RightText.SetActive(false);
        Click.SetActive(false);
        DashText.SetActive(false);
        SoinText.SetActive(false);
        EnrageText.SetActive(false);
        DialogText.SetActive(false);
    }
}
