using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAScript : MonoBehaviour {
	public List<Transform> paths;
	public NavMeshAgent agent;
	public bool hasDestination;
	public Transform destinationCurrent;
	int walk = Animator.StringToHash("Walk");
	int run = Animator.StringToHash("Run");
	int deathAnim = Animator.StringToHash("Die");
	Animator anim;
	public int dgt=20;
	public int healthMax=100;
	public int currentHealth;
    public float currentHealthPercent;
    public RectTransform healthBar;
    public float width;
	public GameObject damagePopup;
    public GameObject zone;
	public GameObject spawn;

    public AudioSource PlayerAudioSource;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;
    public AudioClip hit5;
    public AudioClip hit6;
    public AudioClip bossdeath;
    // Use this for initialization
    void Start () {
        if(gameObject.tag == "ennemySpear")
        {
            hit1 = hit3;
            hit2 = hit4;
        }
        else if (gameObject.tag == "ennemyBossTribu")
        {
            hit1 = hit5;
            hit2 = hit6;
        }

        currentHealthPercent = 100;
        PlayerAudioSource = GameObject.FindGameObjectWithTag("player").GetComponent<AudioSource>();
        paths = new List<Transform> ();
		paths.Add(transform.parent.parent.GetChild(1));
		paths.Add(transform.parent.parent.GetChild(2));
		paths.Add(transform.parent.parent.GetChild(3));
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		currentHealth = healthMax;
		zone = gameObject.transform.parent.parent.GetChild(4).gameObject;
		spawn = gameObject.transform.parent.gameObject;
		zone.SetActive (true);
        
    }

    // Update is called once per frame
    void Update () {

        width = 2 * (currentHealthPercent / 100);
        healthBar.sizeDelta = new Vector2(width, healthBar.sizeDelta.y);
        anim.SetBool (run, false);
		agent.speed = 2;
		if (agent.velocity.magnitude==0)
			anim.SetBool (walk, false);
		else 
			anim.SetBool (walk, true);
		
		if (hasDestination == false) {
			int random = (int)Random.Range (0, 3);
			agent.SetDestination (paths [random].position);
			hasDestination = true;
			destinationCurrent = paths [random];
		} else {
			if (agent.velocity.magnitude==0)
				hasDestination = false;
		}

	}
	public void death() {
		anim.SetBool (run, false);
		anim.SetBool (deathAnim, true);
		if (gameObject.tag == "ennemyOneHand")
			GetComponent<iaAttackScriptOneHand> ().enabled = false;
		else if(gameObject.tag == "ennemyOneHandMace")
			GetComponent<iaAttackScriptOneHandMace> ().enabled = false;
        else if (gameObject.tag == "ennemySpear")
            GetComponent<iaAttackScriptSpear>().enabled = false;
        else if (gameObject.tag == "ennemyDoubleHandAxes")
            GetComponent<iaAttackDoubleHandAxes>().enabled = false;
        else if (gameObject.tag == "ennemyBossTribu")
        {
            GetComponent<iaAttackScriptBossTribu>().OnOff();
            GetComponent<iaAttackScriptBossTribu>().enabled = false;
            PlayerAudioSource.PlayOneShot(bossdeath);
        }
            

        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
        zone.SetActive (false);
		gameObject.GetComponent<CapsuleCollider> ().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<BoxCollider> ().enabled = false;
		spawn.GetComponent<SpawnScript> ().respawn ();
		gameObject.GetComponent<LootEnnemyScript> ().isLooting();
		Destroy (gameObject,10);
	}

	public void takeDamage(int amount, bool isCrit)
	{
        int random = Random.Range(0, 2);
        if(random == 1)
            PlayerAudioSource.PlayOneShot(hit1);
        else
            PlayerAudioSource.PlayOneShot(hit2);
        currentHealth -= amount;
        currentHealthPercent = currentHealth * 100 / healthMax;

        //currentHealthPercent = currentHealth * 100 / maxHealth;
        width = 2 * (currentHealthPercent / 100);
        Debug.Log(width);
		GameObject goPopup = Instantiate (damagePopup);

		goPopup.transform.SetParent(damagePopup.transform.parent);
		goPopup.GetComponent<RectTransform>().anchoredPosition3D = damagePopup.GetComponent<RectTransform>().anchoredPosition3D;
		goPopup.GetComponent<RectTransform>().localScale = damagePopup.GetComponent<RectTransform>().localScale;
		goPopup.GetComponent<RectTransform>().localRotation = damagePopup.GetComponent<RectTransform>().localRotation;

		//goPopup.transform.localRotation = damagePopup.transform.localRotation;
		//goPopup.transform.localScale = damagePopup.transform.localScale;
        if(isCrit)
        {
            goPopup.GetComponent<TMPro.TextMeshProUGUI> ().SetText ("<color=yellow>"+amount+ "</color>");
            goPopup.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 1;
        }
        else
        {
            goPopup.GetComponent<TMPro.TextMeshProUGUI>().SetText("" + amount);
            goPopup.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 0.6f;
        }
        goPopup.GetComponent<Animator> ().SetTrigger ("start");
	
		Destroy (goPopup, 2f);
        healthBar.sizeDelta = new Vector2(width, healthBar.sizeDelta.y);
        if (currentHealth <= 0)
			death ();
	}
}
