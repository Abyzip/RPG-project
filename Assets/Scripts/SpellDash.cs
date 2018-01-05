using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class SpellDash : SpellScript{
	Animator anim;
	int dash;
	public float dashSpeed = 10f;
	public float timer=0f;
	public bool isDashing = false;
	public bool isOnCooldown=false;

    public AudioSource PlayerAudioSource;
    public AudioClip dashSound;



	void Start() {
		anim = player.GetComponent<Animator> ();
		dash = Animator.StringToHash ("Dash");
		gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = img;
		gameObject.transform.GetChild (1).GetComponent<TMPro.TextMeshProUGUI> ().SetText (raccourcis);
	}
	void Update() {
		if (player.GetComponent<RageUIScript> ().currentRage < rageCost) {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,0.5f);
		} else {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,1);;
		}
		if (Input.GetKeyDown (KeyCode.Mouse1)&&isOnCooldown==false) {
			if (player.GetComponent<RageUIScript> ().hasEnoughRage (rageCost)) {
                PlayerAudioSource.PlayOneShot(dashSound);
                anim.SetBool (dash, true);
				isDashing = true;
				StartCoroutine ("timerDashCoroutine");
				isOnCooldown = true;
				StartCoroutine ("timerCooldown");
                

            }
		}

		
		if (isDashing) {

			player.transform.Translate (Vector3.forward * dashSpeed * Time.deltaTime);

		}
	}


	IEnumerator timerDashCoroutine()
	{
		yield return new WaitForSeconds(0.7f);
		isDashing = false;
		anim.SetBool(dash, false);

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


	


