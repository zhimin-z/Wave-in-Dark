using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : EnemyAI {

    // Use this for initialization
    public enum SeekerState
    {
        IDLE,
        ATTACK,
        DEATH
    };

    public SeekerState curState;
    float attackDamage = 20f;
    float proximityRadius = 15f;
    float defaultRadius = 5f; //Assign value
    float attackdist = 2.0f;
    float lightRadius; //Assign value
    float playerDist;

    Vector3 playerPosition;
    NavMeshAgent agent;
    GameObject player;

    void Attack()
    {
        PlayerCharacter.pc.LoseHealthPoint(attackDamage * Time.deltaTime);
        Debug.Log("Player Health" + PlayerCharacter.pc.healthPoint);
    }
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        curState = SeekerState.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] particleshots = GameObject.FindGameObjectsWithTag("ParticleShot");
        playerPosition = player.transform.position;
        playerDist = (playerPosition - transform.position).magnitude;
        
        if (curState == SeekerState.IDLE)
        {
            Wander();
        }
        if (curState == SeekerState.ATTACK )
        {
            //Wander();
            if(playerDist < attackdist)
                Attack();
            else
            {
                //agent.destination = playerPosition;
                if (particleshots.Length > 0)
                {
                    curState = SeekerState.IDLE;
                    agent.destination = particleshots[particleshots.Length - 1].transform.position;
                    
                }
            }
        }
        else
        {
            curState = SeekerState.IDLE;
        }

    }
    
    void Wander()
    {
        //put navmesh to go from left to right
        //find distance between Player and enemy
        GameObject[] particleshots = GameObject.FindGameObjectsWithTag("ParticleShot");
        if (Input.GetButtonDown("Fire2") && playerDist <= proximityRadius)
        {
            curState = SeekerState.ATTACK;
            agent.destination = playerPosition;
            return;
            
        }
        else if (particleshots.Length > 0)
        {
            curState = SeekerState.IDLE;
            agent.destination = particleshots[particleshots.Length -1].transform.position;
            return;
        }
        else
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * defaultRadius;
            randomDirection += transform.position;

            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, defaultRadius, NavMesh.AllAreas);
            agent.destination = navHit.position;
            curState = SeekerState.IDLE;
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            curState = SeekerState.ATTACK;//then play the attack animation
        }
    }

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player")
		{
			curState = SeekerState.IDLE;//then play the attack animation
		}
	}
}
