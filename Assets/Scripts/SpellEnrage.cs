using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class SpellEnrage : SpellScript {

	public bool isOnCooldown=false;
	public bool isEnrage=false;
	public Texture imgEnrage;
	public float timerLerp=0;

	public int oldRageCost;

    public AudioSource PlayerAudioSource;
    public AudioClip rageSound;
    void Start() {

		gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = img;
		gameObject.transform.GetChild (1).GetComponent<TMPro.TextMeshProUGUI> ().SetText (raccourcis);
		oldRageCost = rageCost;
	}
	void Update() {
		if (isEnrage) {
			rageCost = 0;
		} else {
			rageCost = oldRageCost;
		}
		if (player.GetComponent<RageUIScript> ().currentRage < rageCost ||player.GetComponent<HealthUIScript> ().currentHealthPercent < 20) {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,0.5f);
		} else {
			gameObject.transform.GetChild (0).GetComponent<RawImage> ().color = new Color(1,1,1,1);;
		}
		if (Input.GetKeyDown (KeyCode.Tab)&&isOnCooldown==false&&player.GetComponent<HealthUIScript> ().currentHealthPercent >= 20) {
			if (player.GetComponent<RageUIScript> ().hasEnoughRage (rageCost)) {
                PlayerAudioSource.PlayOneShot(rageSound);
                isOnCooldown = true;

				if (isEnrage == false) {
					isEnrage = true;
					gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = imgEnrage;
					StartCoroutine ("timerCooldown");
					StartCoroutine ("timerIncrementEnrageOn");
					StartCoroutine ("isEnrageEffect");
					player.GetComponent<EquipmentsPlayer> ().dgtLeft = (int)player.GetComponent<EquipmentsPlayer> ().dgtLeft*2;
					player.GetComponent<EquipmentsPlayer> ().dgtRight = (int)player.GetComponent<EquipmentsPlayer> ().dgtRight*2;
				} else
                {
                    UnRage();
                }

            }
		}

	}

    public void UnRage()
    {
            isEnrage = false;
        gameObject.transform.GetChild(0).GetComponent<RawImage>().texture = img;
        StartCoroutine("timerCooldown");
        StartCoroutine("timerIncrementEnrageOff");
        player.GetComponent<EquipmentsPlayer>().dgtLeft = (int)player.GetComponent<EquipmentsPlayer>().dgtLeft / 2;
        player.GetComponent<EquipmentsPlayer>().dgtRight = (int)player.GetComponent<EquipmentsPlayer>().dgtRight / 2;
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

	IEnumerator timerIncrementEnrageOn()
	{
		
		while (timerLerp < 1) {
			timerLerp += Time.deltaTime;
			yield return new WaitForSeconds(0.01f);
			player.transform.localScale = Vector3.Lerp (player.transform.localScale, new Vector3 (2, 2, 2), timerLerp);

		}

		timerLerp = 0;
	}

	IEnumerator timerIncrementEnrageOff()
	{

		while (timerLerp < 1) {
			timerLerp += Time.deltaTime;
			yield return new WaitForSeconds(0.01f);
			player.transform.localScale = Vector3.Lerp (player.transform.localScale, new Vector3 (1.5f, 1.5f, 1.5f), timerLerp);

		}

		timerLerp = 0;
	}

	IEnumerator isEnrageEffect()
	{
		while (isEnrage) {
			player.GetComponent<HealthUIScript> ().takeDamage (10);
			yield return new WaitForSeconds(1);
			Debug.Log (player.GetComponent<HealthUIScript> ().currentHealthPercent);
			if (player.GetComponent<HealthUIScript> ().currentHealthPercent < 20) {
				isEnrage = false;
				gameObject.transform.GetChild (0).GetComponent<RawImage> ().texture = img;
				StartCoroutine ("timerCooldown");
				StartCoroutine ("timerIncrementEnrageOff");
				player.GetComponent<EquipmentsPlayer> ().dgtLeft = (int)player.GetComponent<EquipmentsPlayer> ().dgtLeft/2;
				player.GetComponent<EquipmentsPlayer> ().dgtRight = (int)player.GetComponent<EquipmentsPlayer> ().dgtRight/2;
			}
		}
			
	}
}

