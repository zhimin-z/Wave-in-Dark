using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFire : MonoBehaviour {
	public static PrimaryFire primaryFire;
	// public
	public GameObject particleShot;

	public float maxHeat;
	public float heating_speed;
	public float cooling_speed;
	public float cooldownTime;
	[HideInInspector]
	public float currentHeat = 0;

	// private
	bool isHeated;

    [SerializeField] private AudioClip m_ParticleShotSound;
    [SerializeField] private AudioClip m_ParticleHeatSound;
    private AudioSource m_AudioSource;

    void Awake(){
		primaryFire = this;
	}

	// Use this for initialization
	void Start () {
        m_AudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (currentHeat > 0) {
			currentHeat -= Time.deltaTime * cooling_speed;
			if (currentHeat < 0) {
				currentHeat = 0;
			}
		}
	}

	public void Fire(){
		if (currentHeat < maxHeat && !isHeated) {
			GameObject newParticleShot = GameObject.Instantiate (particleShot, Camera.main.transform.position + Camera.main.transform.forward * 1.0f, Quaternion.identity);
			ParticleShotScript particleShotScript = newParticleShot.GetComponent<ParticleShotScript> ();
			particleShotScript.direction = Camera.main.transform.forward;
            PlayParticleShotSound();
            currentHeat += heating_speed;
			if (currentHeat >= maxHeat) {
				isHeated = true;
                PlayParticleHeatSound();
                StartCoroutine ("coolingDown");
			}
		}
		Debug.Log ("Primary Fire -- currentHeat = " + currentHeat);
	}

    public void PlayParticleShotSound()
    {
        if (m_ParticleShotSound)
        {
            m_AudioSource.PlayOneShot(m_ParticleShotSound);
        }
    }

    public void PlayParticleHeatSound()
    {
        if (m_ParticleHeatSound)
        {
            m_AudioSource.PlayOneShot(m_ParticleHeatSound);
        }
    }

    IEnumerator coolingDown(){
		
		yield return new WaitForSeconds (cooldownTime);

		isHeated = false;
	}
}
