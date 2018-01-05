using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerScript : MonoBehaviour {
    int attackRight = Animator.StringToHash("Melee Right Attack 03");
    int attackLeft = Animator.StringToHash("Melee Left Attack 01");
    public bool alternateAttack=false;
    Animator anim;
	public int dgtLeft;
	public int dgtRight;
	public int currentDgt;
    public bool canAttack=true;

    public AudioSource PlayerAudioSource;
    public AudioClip attack;
    public AudioClip attack2;
    
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
        if (canAttack == true)
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                canAttack = false;
                StartCoroutine("timerAttack");


                if (alternateAttack)
                {
                    PlayerAudioSource.PlayOneShot(attack);
                    anim.SetTrigger(attackLeft);
                    currentDgt = GetComponent<EquipmentsPlayer>().dgtLeft;
                    StartCoroutine("takeDamageCoroutine");
                    alternateAttack = false;
                }
                else
                {
                    PlayerAudioSource.PlayOneShot(attack2);
                    anim.SetTrigger(attackRight);
                    currentDgt = GetComponent<EquipmentsPlayer>().dgtRight;
                    StartCoroutine("takeDamageCoroutine");
                    alternateAttack = true;
                }
        }
        }


    }

	IEnumerator takeDamageCoroutine() {
		yield return new WaitForSeconds (0.5f);

		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast (transform.position+new Vector3(0,1,0), fwd, out hit)) {
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag.StartsWith("ennemy")) {
					if (hit.distance < 2) {
                        int criticalStrike = GetComponent<EquipmentsPlayer>().criticalStrike;
                        bool isCrit = false;
                        int random = Random.Range(0, 101);
                        if (random <= criticalStrike)
                        {
                            currentDgt = currentDgt * 2;
                            isCrit = true;
                        }
                        hit.collider.gameObject.GetComponent<IAScript> ().takeDamage(currentDgt, isCrit);
						GetComponent<RageUIScript> ().gainRage (5);
					}
				}
			}
		}
	}

    IEnumerator timerAttack()
    {
        yield return new WaitForSeconds(0.5f);

        canAttack = true;
    }
}
