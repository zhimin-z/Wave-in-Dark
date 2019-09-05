using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canceller : MonoBehaviour {

	public Shader replacementShader;
	public Camera playerCamera;
	public float growthRate;
	public float fadeRate;
	public Vector3 maxScale;
	public bool alerted;
	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		alerted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (alerted) {
			if (gameObject.transform.localScale.x < maxScale.x) {
				gameObject.transform.localScale += growthRate * maxScale * Time.deltaTime;
				GetComponent<Renderer> ().materials [0].color += new Color (0, 0, 0, growthRate * Time.deltaTime);
			} else if (GetComponent<Renderer> ().materials [0].color.a > 0.01) {
				GetComponent<Renderer> ().materials [0].color -= new Color (0, 0, 0, fadeRate * Time.deltaTime);
			} else {
				gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
				alerted = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			playerCamera.SetReplacementShader(replacementShader,"");
	}

	void OnTriggerExit(Collider other)
	{	
		if(other.tag == "Player")
			playerCamera.ResetReplacementShader ();
	}
}
