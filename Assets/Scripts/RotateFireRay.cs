using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFireRay : MonoBehaviour {
	public float speed;

    public AudioSource TotemAudioSource;
    public AudioClip totemFire;
    // Use this for initialization
    void Start () {
        TotemAudioSource.clip = totemFire;
        TotemAudioSource.Play();
        StartCoroutine ("speedBoost");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (0, 1*Time.deltaTime*speed, 0);
	}

	IEnumerator speedBoost()
	{
		while (true) {
			yield return new WaitForSeconds (10f);
			speed += 2;
		}

	}


}
