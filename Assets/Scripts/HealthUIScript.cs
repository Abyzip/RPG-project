using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIScript : MonoBehaviour {
    public int maxHealth=100;
    public int currentHealth;
    public RectTransform healthBar;
    public int currentHealthPercent;
	public GameObject spawn;

    public AudioSource PlayerAudioSource;
    public AudioClip hit1;
    public AudioClip hit2;

    private bool isHit = true;
    void Start()
    {
        maxHealth = currentHealth;
    }


    public void heal(int amount)
    {
        currentHealth += amount;
        currentHealthPercent = currentHealth * 100 / maxHealth;
        if (currentHealth > maxHealth)
        { 
            currentHealth = maxHealth;
            currentHealthPercent = 100;
        }
    }

    public void takeDamage(int amount)
    {
        if (isHit)
        {
            PlayerAudioSource.PlayOneShot(hit1);
            isHit = false;
        }
        else
        {
            PlayerAudioSource.PlayOneShot(hit2);
            isHit = true;
        }

        currentHealth -= amount;
        currentHealthPercent = currentHealth * 100 / maxHealth;
		if (currentHealth < 0)
			death ();

    }

	public void death()
	{
		currentHealth =maxHealth;
		currentHealthPercent = 100;
		transform.position = spawn.transform.position;
    }

    // Update is called once per frame
    void Update () {
        
        healthBar.sizeDelta = new Vector2(currentHealthPercent, healthBar.sizeDelta.y);
    }
}


