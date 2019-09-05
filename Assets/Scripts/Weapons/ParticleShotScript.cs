using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShotScript : MonoBehaviour {

    public Vector3 direction = new Vector3(0,0,1);
    public float force = 100;
    public float lifeTimeInSeconds = 5.0f;
    public float damage = 10.0f;

    float gravitation = 9.8f;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        
        // Push in the first
        rigidBody.AddForce(direction * force, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        lifeTimeInSeconds -= Time.deltaTime;
        if(lifeTimeInSeconds < 0.0f)
        {
            DestroyObject(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //print("holy");
        
        //print(collision.gameObject.name);
        if(collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            //print("shit!!!!");
            //collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
			Debug.Log("zombie health = " + collision.gameObject.GetComponent<EnemyAI>().curHealth);
        }
		if(collision.collider.tag != "Player")
		DestroyObject(gameObject);
    }



}
