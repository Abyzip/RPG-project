using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellHeal : SpellScript {



	public bool isOnCooldown=false;
	public GameObject particleHeal;
	public int amountHeal;

    public AudioSource PlayerAudioSource;
    public AudioClip healSound;

    void Start() {

		gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = img;
		gameObject.transform.GetChild (1).GetComponent<TMPro.TextMeshProUGUI> ().SetText (raccourcis);
	}
	void Update() {
		if (player.GetComponent<RageUIScript> ().currentRage < rageCost) {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,0.5f);
		} else {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,1);;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)&&isOnCooldown==false) {
			if (player.GetComponent<RageUIScript> ().hasEnoughRage (rageCost)) {
                PlayerAudioSource.PlayOneShot(healSound);
                isOnCooldown = true;
				GameObject go=Instantiate (particleHeal);
				Destroy (go, 2f);
				go.transform.parent = player.transform;
				go.transform.localPosition = new Vector3 (0, 1, 0);

				StartCoroutine ("timerCooldown");
				player.GetComponent<HealthUIScript> ().heal (amountHeal);
			}
		}
			
	}




	IEnumerator timerCooldown()
	{
		gameObject.transform.GetChild (2).gameObject.SetActive (true);
		TMPro.TextMeshProUGUI textGO=gameObject.transform.GetChild (2).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI> ();
		for (int i = cooldown; i > 0; i--) {
			textGO.SetText ("" + i);
			yield return new WaitForSeconds(1);
		}
		gameObject.transform.GetChild (2).gameObject.SetActive (false);
		isOnCooldown = false;
	}
}
