using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SeekerAI : EnemyAI
{
    public enum SeekerState
    {
        IDLE, 
        ATTACK, 
        DEATH
    };

    public SeekerState curState;
	public GameObject PowerUp;


    float attackDamage = 20f;
    float proximityRadius = 15f;
    float defaultRadius = 5f; //Assign value
    float lightRadius; //Assign value

	float AttackRange = 2.0f;
    float playerDist;

    Vector3 playerPosition;
    NavMeshAgent agent;
    GameObject player;

	int frameCount = 0;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        curState = SeekerState.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = player.transform.position;
        playerDist = (playerPosition - transform.position).magnitude;

		if (playerDist <= AttackRange) {
			curState = SeekerState.ATTACK;
		} else {
			curState = SeekerState.IDLE;
		}

        if (curHealth <= 0)
        {
            curState = SeekerState.DEATH;//then play the death animation

			GameObject.Instantiate (PowerUp, transform.position, transform.rotation);

            Destroy(gameObject);
        }

		if (curState == SeekerState.IDLE)
        {
            Wander();
        }
        
		else if(curState == SeekerState.ATTACK){
			if (PlayerCharacter.pc.healthPoint > 0) {
				Attack ();
			}
		}
        
	}

    void Wander()
    {
        //put navmesh to go from left to right
        //find distance between Player and enemy
        if (playerDist <= proximityRadius)
        {
            //curState = SeekerState.ATTACK;
            agent.destination = playerPosition;
            return;
        }
        else {
            Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * defaultRadius;
            randomDirection += transform.position;

            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, defaultRadius, NavMesh.AllAreas);
            agent.destination = navHit.position;
        }
    }

    IEnumerator Damage()
    {
        PlayerCharacter.pc.LoseHealthPoint(attackDamage * Time.deltaTime);
		Debug.Log ("player health = " + PlayerCharacter.pc.healthPoint);
		yield return new WaitForSeconds (0.25f);
    }

	void Attack(){
		
		PlayerCharacter.pc.LoseHealthPoint(attackDamage * Time.deltaTime);
		//Debug.Log ("player health = " + PlayerCharacter.pc.healthPoint);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            curState = SeekerState.ATTACK;//then play the attack animation
           // StartCoroutine(Damage());
        }
    }

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "ParticleShot")
        {
           TakeDamage(10.0f);//Or other.gameObject.particleDamage
			Debug.Log("zombie health = " + curHealth);
        }
    }
}
