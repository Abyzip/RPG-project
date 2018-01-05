using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZoneIAScript : MonoBehaviour {
	public GameObject pnj;
	public string pnjTag;
	// Use this for initialization

	public GameObject Player;

	public bool isAggro=false;

	// Use this for initialization
	void Start () {
		

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter(Collider other) {

        if (other.tag == "player")
        {
            pnj = transform.parent.GetChild(0).GetChild(0).gameObject;
            pnjTag = pnj.tag;
            pnj.GetComponent<IAScript>().enabled = false;
            if (pnjTag == "ennemyOneHand")
            {
                pnj.GetComponent<iaAttackScriptOneHand>().player = other.gameObject;
                pnj.GetComponent<iaAttackScriptOneHand>().enabled = true;
            }
            else if (pnjTag == "ennemyOneHandMace")
            {
                pnj.GetComponent<iaAttackScriptOneHandMace>().player = other.gameObject;
                pnj.GetComponent<iaAttackScriptOneHandMace>().enabled = true;
            }
            else if (pnjTag == "ennemySpear")
            {
                pnj.GetComponent<iaAttackScriptSpear>().player = other.gameObject;
                pnj.GetComponent<iaAttackScriptSpear>().enabled = true;
            }
            else if (pnjTag == "ennemyDoubleHandAxes")
            {
                pnj.GetComponent<iaAttackDoubleHandAxes>().player = other.gameObject;
                pnj.GetComponent<iaAttackDoubleHandAxes>().enabled = true;
            }
            else if (pnjTag == "ennemyBossTribu")
            {
                pnj.GetComponent<iaAttackScriptBossTribu>().player = other.gameObject;
                pnj.GetComponent<iaAttackScriptBossTribu>().enabled = true;
                pnj.GetComponent<iaAttackScriptBossTribu>().engageBattle();
            }
        }
        }

	void OnTriggerExit(Collider other) {
		
		if (other.tag=="player") {
            
            pnj.GetComponent<IAScript>().enabled = true;
            pnj.GetComponent<IAScript>().hasDestination = false;
            pnj.GetComponent<IAScript>().currentHealth = pnj.GetComponent<IAScript>().healthMax;
            pnj.GetComponent<IAScript>().currentHealthPercent = 100;
            if (pnjTag == "ennemyOneHand") {
				pnj.GetComponent<iaAttackScriptOneHand>().enabled = false;
			} else if (pnjTag == "ennemyOneHandMace") {
				pnj.GetComponent<iaAttackScriptOneHandMace> ().enabled = false;
			}
			else if (pnjTag == "ennemySpear") {
				pnj.GetComponent<iaAttackScriptSpear> ().enabled = false;
			}
            else if (pnjTag == "ennemyDoubleHandAxes")
            {
                pnj.GetComponent<iaAttackDoubleHandAxes>().enabled = false;
            }
            else if (pnjTag == "ennemyBossTribu")
            {
                Debug.Log("sorti");
                pnj.GetComponent<iaAttackScriptBossTribu>().OnOff();
                pnj.GetComponent<iaAttackScriptBossTribu>().enabled = false;
            }


        }
	}




}
