using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaAttackScriptBossTribu : MonoBehaviour {
    public NavMeshAgent agent;
    int run = Animator.StringToHash("Run");
    int walk = Animator.StringToHash("Walk");
    int attackRight = Animator.StringToHash("Right Punch Attack");
    int attackLeft = Animator.StringToHash("Left Punch Attack");
    Animator anim;
    public GameObject player;
    public int speedRun;
    public bool canAttack = true;
    public float attackSpeed = 1.5f;
    public int damage;
    public bool alternateAttack = true;
	public GameObject door;
	public GameObject rays;
	private Vector3 startPositionDoor;
	private Vector3 endPositionDoor;

    void Start()
    {
        rays = GameObject.FindGameObjectWithTag("raysBoss");
        door = GameObject.FindGameObjectWithTag("bossDoor");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        damage = GetComponent<IAScript>().dgt;
        startPositionDoor = door.transform.position;
        endPositionDoor = startPositionDoor + new Vector3(0, 6, 0);
        engageBattle();
    }

    public void OnOff()
    {
        //rays = GameObject.FindGameObjectWithTag("raysBoss");
        //door = GameObject.FindGameObjectWithTag("bossDoor");
        //agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();
        //damage = GetComponent<IAScript>().dgt;
        //startPositionDoor = door.transform.position;
        //endPositionDoor = startPositionDoor + new Vector3(0, 6, 0);
        Debug.Log(startPositionDoor);
        StartCoroutine("desengageBattle");
        Debug.Log(startPositionDoor);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            if (canAttack == true)
            {
                transform.LookAt(player.transform);
                anim.SetBool(run, false);
                if (alternateAttack)
                {
                    anim.SetTrigger(attackRight);
                    alternateAttack = false;
                }
                else
                {
                    anim.SetTrigger(attackLeft);
                    alternateAttack = true;
                }

                StartCoroutine("takeDamageCoroutine");

                canAttack = false;
                StartCoroutine("attackSpeedCoroutine");
            }

        }
        else
        {
            agent.SetDestination(player.transform.position);

            agent.speed = speedRun;
            anim.SetBool(walk, false);
            anim.SetBool(run, true);
        }

    }

    IEnumerator attackSpeedCoroutine()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    public void engageBattle()
    {
        StartCoroutine("doorClose");
    }

    public void desengageBattle()
    {
        StartCoroutine("doorOpen");
    }


    IEnumerator doorClose()
	{
		float timer = 0;
		yield return new WaitForSeconds(0.5f);
		rays.transform.GetChild(1).gameObject.SetActive (true);
		while (timer < 1) {

			yield return new WaitForSeconds(0.01f);
			timer += 0.03f;
			door.transform.position = Vector3.Lerp (startPositionDoor, endPositionDoor, timer);
		}

	}

    IEnumerator doorOpen()
    {
        float timer = 0;
        yield return new WaitForSeconds(0.5f);
        rays.transform.GetChild(1).gameObject.SetActive(false);
        while (timer < 1)
        {

            yield return new WaitForSeconds(0.01f);
            timer += 0.03f;
            door.transform.position = Vector3.Lerp(endPositionDoor, startPositionDoor, timer);
        }

    }

    IEnumerator takeDamageCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), fwd, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "player")
                {
                    if (hit.distance < 3)
                        hit.collider.gameObject.GetComponent<HealthUIScript>().takeDamage(damage);
                }
            }
        }



    }
}
