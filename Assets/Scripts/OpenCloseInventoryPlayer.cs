using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventoryPlayer : MonoBehaviour {

	public GameObject panelInventory;
    public GameObject beanText;

    public GameObject spellDash;
    public GameObject spellHeal;
    public GameObject spellRage;
    public GameObject toolTip;

    void Start () {


		panelInventory.SetActive (false);
		
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
		{
			if (panelInventory.activeSelf == true) {
				panelInventory.SetActive (false);
                beanText.SetActive(false);
                Cursor.visible = false;
				GetComponent<PlayerController> ().enabled = true;
				GetComponent<AttackPlayerScript> ().enabled = true;
                spellDash.GetComponent<SpellDash>().enabled = true;
                spellHeal.GetComponent<SpellHeal>().enabled = true;
                spellRage.GetComponent<SpellEnrage>().enabled = true;
                toolTip.SetActive(false);
            } else {
				//PlayerController, 
				panelInventory.SetActive (true);
                beanText.SetActive (true);
                Cursor.visible = true;
				GetComponent<PlayerController> ().enabled = false;
				GetComponent<AttackPlayerScript> ().enabled = false;
                spellDash.GetComponent<SpellDash>().enabled = false;
                spellHeal.GetComponent<SpellHeal>().enabled = false;
                if(spellRage.GetComponent<SpellEnrage>().isEnrage == true)
                {
                    spellRage.GetComponent<SpellEnrage>().UnRage();
                }
                
                spellRage.GetComponent<SpellEnrage>().enabled = false;
            }

		}
	}
}
