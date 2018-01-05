using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaAttackDoubleHandAxes : MonoBehaviour {
    public NavMeshAgent agent;
    int run = Animator.StringToHash("Run");
    int walk = Animator.StringToHash("Walk");
    int attackRight = Animator.StringToHash("Melee Right Attack 03");
    int attackLeft = Animator.StringToHash("Melee Left Attack 01");
    Animator anim;
    public GameObject player;
    public int speedRun;
    public bool canAttack = true;
    public float attackSpeed = 1.5f;
    public int damage;
    public bool alternateAttack = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        damage = GetComponent<IAScript>().dgt;

    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
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
                    if (hit.distance < 2)
						hit.collider.gameObject.GetComponent<HealthUIScript>().takeDamage(damage);
                }
            }
        }



    }
}
