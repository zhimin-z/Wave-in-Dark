using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancellerTrigger : MonoBehaviour {

	public GameObject darkWave;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 0.1f);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag == "LightGunBullet") {
				darkWave.GetComponent<Canceller> ().alerted = true;
			}
		}
	}

}
