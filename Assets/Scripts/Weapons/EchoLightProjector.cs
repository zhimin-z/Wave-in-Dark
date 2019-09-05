using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLightProjector : MonoBehaviour {

	public Projector projectorComponent;
	public float rateOfSizeIncrease = 1.0f;
	public float rateOfIntensityDecrease = 100.0f;
	public float timeAlive = 4.0f;
	// Use this for initialization
	void Start () {
		this.GetComponent<Projector> ().orthographicSize = 1.0f;
		rateOfSizeIncrease = 1.0f;
		rateOfIntensityDecrease = 100.0f;
		timeAlive = 4.0f;
		StartCoroutine("Destroy");
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Projector> ().orthographicSize += (rateOfSizeIncrease*Time.deltaTime);
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(timeAlive);
		DestroyObject (this.gameObject);
	}
}
